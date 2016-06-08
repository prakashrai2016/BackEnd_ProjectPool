using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectPool.Library;
using ProjectPool.DataEntity;
using ProjectPool.Repository;
using ProjectPool.Models;
using System.IO;

namespace ProjectPool.Services
{
    public class ProjectStepService : GenericRepository<ProjectStep>
    {
        string ConnectionString;
        public ProjectStepService(string connectionString)
            : base(connectionString)
        {
            ConnectionString = connectionString;
        }

        public bool SaveSteps(ProjectStepList steps)
        {
            var projectName = new ProjectService(ConnectionString).Load(x => x.ProjectId == steps.projectId).Select(x => x.Title).FirstOrDefault().Replace(" ", "_");
            int stepNo = 1;

            try
            {
                foreach (var step in steps.projectStepList)
                {
                    string fileName = "Step" + stepNo.ToString() + "_" + step.Title + "." + step.uploadStepImage.FileName.Split('.')[1];
                    fileName = fileName.Replace(" ", "_");

                    string path = Common.GetAppSettingValue("ProjectFilePath");
                    path = Path.Combine(HttpContext.Current.Server.MapPath(path), projectName);

                    Common.UploadFile1(step.uploadStepImage, path, fileName, "Project", false);
                    step.ImageName = fileName;
                    step.Step = stepNo;
                    AddStep(step, steps.projectId);

                    stepNo += 1; 
                }
                
                return true;
            }

            catch {
                return false;
            }
            
        }

        public void AddStep(ProjectStepViewModel step, int projectId)
        {
            ProjectPoolDbConnection context = new ProjectPoolDbConnection();
            ProjectStep newStep = new ProjectStep() {
                ProjectId = projectId, 
                GUID = Guid.NewGuid().ToString(),
                Step = step.Step,
                Title = step.Title,
                Description = step.Description,
                ImageName = step.ImageName,
                DateCreated = DateTime.Now
            };

            Add(newStep);
            SaveChanges();
        }

        public int Save(ProjectStep projectStep, bool isEdit)
        {
            try
            {
                var projectName = new ProjectService(this.ConnectionString).Load(x => x.ProjectId == projectStep.ProjectId).Select(x => x.Title).SingleOrDefault();
                var request = HttpContext.Current.Request;
                var ProjectImage = request.Files["uploadProjectStepImage"] as HttpPostedFile;

                if (ProjectImage != null && ProjectImage.FileName != "")
                {


                    string fileName = projectStep.Title + "_" + DateTime.Now.ToString("yyMMdd") + "." + ProjectImage.FileName.Split('.')[1];
                    fileName = fileName.Replace(" ", "_");

                    string path = Common.GetAppSettingValue("ProjectFilePath");
                    path = Path.Combine(HttpContext.Current.Server.MapPath(path), projectName.Replace(" ","_"));

                    if (isEdit)
                    {
                        Common.DeleteFile(projectStep.ImageName, path);
                    }

                    Common.UploadFile(ProjectImage, path, fileName, "Project", true);
                    projectStep.ImageName = fileName;
                }


                if (isEdit)
                {
                    projectStep.DateModified = DateTime.Now;
                    Update(projectStep);

                }
                else
                {
                    projectStep.GUID = Guid.NewGuid().ToString();
                    projectStep.DateCreated = DateTime.Now;
                    Add(projectStep);
                }


                SaveChanges();
                return 1;
            }
            catch { return 0; }
        }

        public void Delete(int projectId)
        {
            var stepList = Load(x => x.ProjectId == projectId).ToList();
            foreach (var step in stepList)
            {
                Remove(step);
                SaveChanges();
            }
        }
    }
}