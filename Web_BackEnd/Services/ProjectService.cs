using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectPool.Repository;
using ProjectPool.DataEntity;
using System.Data.Entity;
using ProjectPool.Library;
using ProjectPool.Models;
using System.IO;

namespace ProjectPool.Services
{
    public class ProjectService : GenericRepository<Project>
    {
        string ConnectionString;

        GenericRepository<Project> repository = new GenericRepository<Project>(new DbContext(Common.GetConnectionString()));

        public ProjectService(string ConnectionString)
            : base(ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }

        public ProjectService(DbContext context)
            : base(context)
        {
            this.ConnectionString = context.Database.Connection.ConnectionString;
        }

        public IEnumerable<Project> GetAll()
        {
            return this.Load();
        }

        public Project GetProjectById(int projectId)
        {
            return Load(x => x.ProjectId == projectId).SingleOrDefault();
        }

        public Project GetProjectByGuid(string guid)
        {
            return Load(x => x.GUID == guid).FirstOrDefault();
        }

        public int Save(Project Project, bool isEdit)
        {
            try
            {
                var request = HttpContext.Current.Request;
                var ProjectImage = request.Files["uploadProjectImage"] as HttpPostedFile;

                if (ProjectImage != null && ProjectImage.FileName != "")
                {


                    string fileName = Project.Title + "_" + DateTime.Now.ToString("yyMMdd") + "." + ProjectImage.FileName.Split('.')[1];
                    fileName = fileName.Replace(" ", "_");

                    string path = Common.GetAppSettingValue("ProjectFilePath");
                    path = Path.Combine(HttpContext.Current.Server.MapPath(path), Project.Title.Replace(' ', '_'));

                    if (isEdit)
                    {
                        Common.DeleteFile(Project.ImageName, path);
                    }

                    Common.UploadFile(ProjectImage, path, fileName, "Project", true);
                    Project.ImageName = fileName;
                }


                if (isEdit)
                {
                    Project.DateModified = DateTime.Now;
                    Update(Project);

                }
                else
                {
                    Project.GUID = Guid.NewGuid().ToString();
                    Project.DateCreated = DateTime.Now;
                    Project.Views = 100;
                    Add(Project);
                }


                SaveChanges();

                int categoryId = new CategoryService(ConnectionString).GetCateGoryId("Project");
                string ProjectTagLinks = request.Form["ProjectTags"].ToString();

                if (isEdit)
                {
                    var tagLinkService = new TagLinkService(ConnectionString);
                    var tagList = tagLinkService.Load(x => x.LinkId == Project.ProjectId && x.CategoryId == categoryId).ToList();
                    var removeTagList = (from q in tagList
                                        where !ProjectTagLinks.Contains(q.TagId.ToString())
                                        select q.TagLinkId).ToList();

                    foreach (var tag in removeTagList)
                    {
                        tagLinkService.DeleteLinks(tag);
                    }
                }
                
                new TagLinkService(ConnectionString).AddTagLinks(ProjectTagLinks, categoryId, Project.ProjectId);
                
                return Project.ProjectId;
            }
            catch { return 0; }
        }

        
        public void EditProject(Project project)
        {
            Add(project);
            SaveChanges();
        }


        public void Delete(int ProjectId)
        {
            var Project = Load(x => x.ProjectId == ProjectId).FirstOrDefault();
            string path = Common.GetAppSettingValue("ProjectFilePath");
            path = Path.Combine(HttpContext.Current.Server.MapPath(path), Project.Title.Replace(' ', '_'));
            Common.DeleteFile(Project.ImageName, path);
            Remove(Project);
            SaveChanges();
        }

        public string UploadTempImage()
        {
            var request = HttpContext.Current.Request;
            var projectImage = request.Files["upload"] as HttpPostedFile;

            string fileName = projectImage.FileName.Split('.')[0] + "_" + DateTime.Now.ToString("yyMMdd") + "." + projectImage.FileName.Split('.')[1];
            fileName = fileName.Replace(" ", "_");

            string path = Common.GetAppSettingValue("ProjectFilePath");
            path = Path.Combine(HttpContext.Current.Server.MapPath(path), "Temp");

            Common.UploadFile(projectImage, path, fileName, "Project", false);

            return Common.GetAppSettingValue("BaseUrl") + "Data//Project//Temp//" + fileName;
        }
    }

}