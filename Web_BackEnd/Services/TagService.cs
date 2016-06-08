using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectPool.DataEntity;
using ProjectPool.Repository;
using ProjectPool.Library;
using System.IO;

namespace ProjectPool.Services
{
    public class TagService : GenericRepository<Tag>
    {
        string ConnectionString;

        public TagService(string connectionString)
            : base(connectionString)
        { }

        public Dictionary<int, string> GetTagKeyValueList()
        {
            return Load(x => x.DateDeleted == null).ToDictionary(q => q.TagId, q => q.Name);
        }

        public List<Tag> GetAll()
        {
            return Load().ToList();
        }

        public bool Save(Tag tag, bool isEdit)
        {
            try
            {
                var request = HttpContext.Current.Request;
                var articleImage = request.Files["uploadTagImage"] as HttpPostedFile;

                if (articleImage != null && articleImage.FileName != "")
                {


                    string fileName = tag.Name + "_" + DateTime.Now.ToString("yyMMdd") + "." + articleImage.FileName.Split('.')[1];
                    fileName = fileName.Replace(" ", "_");

                    string path = Common.GetAppSettingValue("TagFilePath");
                    path = Path.Combine(HttpContext.Current.Server.MapPath(path), tag.Name.Replace(' ', '_'));

                    if (isEdit)
                    {
                        Common.DeleteFile(tag.ImageName, path);
                    }

                    Common.UploadFile(articleImage, path, fileName, "Tag", true);
                    tag.ImageName = fileName;
                }


                if (isEdit)
                {
                    tag.DateModified = DateTime.Now;
                    Update(tag);

                }
                else
                {
                    tag.GUID = Guid.NewGuid().ToString();
                    tag.DateCreated = DateTime.Now;
                    Add(tag);
                }


                SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public void Delete(int id)
        {
            var tag = Load(x => x.TagId == id).FirstOrDefault();
            Remove(tag);
            SaveChanges();
        }
    }
}