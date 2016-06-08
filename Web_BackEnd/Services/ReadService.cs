using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectPool.Repository;
using ProjectPool.DataEntity;
using ProjectPool.Library;
using System.IO;

namespace ProjectPool.Services
{
    public class ReadService : GenericRepository<Read> 
    {
        string _ConnectionString;
        public ReadService(string connectionString)
            : base(connectionString)
        {
            _ConnectionString = connectionString;
        }

        public IEnumerable<Read> GetAll()
        {
            return this.Load();
        }

        public bool Save(Read read, bool isEdit)
        {
            try
            {
                var request = HttpContext.Current.Request;
                var articleImage = request.Files["uploadReadImage"] as HttpPostedFile;

                if (articleImage != null && articleImage.FileName != "")
                {
                    string fileName = "Read_" + articleImage.FileName.Split('.')[0] + DateTime.Now.ToString("yyMMdd") + "." + articleImage.FileName.Split('.')[1];
                    fileName = fileName.Replace(" ", "_");

                    string path = Common.GetAppSettingValue("ReadFilePath");
                    path = Path.Combine(HttpContext.Current.Server.MapPath(path), read.Topic.ToString());

                    if (isEdit)
                    {
                        Common.DeleteFile(read.ImageName, path);
                    }

                    Common.UploadFile(articleImage, path, fileName, "Read", true);
                    read.ImageName = fileName;
                }

                if (isEdit)
                {
                    read.DateModified = DateTime.Now;
                    Update(read);
                }
                else
                {
                    read.GUID = Guid.NewGuid().ToString();
                    read.DateCreated = DateTime.Now;
                    Add(read);    
                }
                
                SaveChanges();
                return true;
            }

            catch { return false; }
        }

        public void Delete(int readId)
        {
            var read = Load(x => x.ReadId == readId).FirstOrDefault();
            string path = Common.GetAppSettingValue("ReadFilePath");
            path = Path.Combine(HttpContext.Current.Server.MapPath(path), readId.ToString());
            Common.DeleteFile(read.ImageName, path);
            Remove(read);
            SaveChanges();
        }
    }
}