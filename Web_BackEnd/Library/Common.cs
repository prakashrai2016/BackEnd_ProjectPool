using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using ProjectPool.DataEntity;
using ProjectPool.Repository;

namespace ProjectPool.Library
{
    public static class Common
    {
        public static string GetAppSettingValue(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString().Trim();
        }

        public static string GetDefaultConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ProjectPoolDBEntities"].ConnectionString.ToString();
        }


        public static bool CheckUserSession()
        {
            if (HttpContext.Current.Session["UserId"] != null)
            {
                return true;
            }
            else
            {
                HttpContext.Current.Session.Abandon();
                return false;
            }
        }

        public static string UploadFile(HttpPostedFile file, string FilePath, string fileName, string category, bool saveIcon)
        {
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            
            string fileLocation = Path.Combine(FilePath, fileName);
            file.SaveAs(fileLocation);
            if (saveIcon)
            {
                SaveImageAsIcon(file,FilePath, fileName);
            }
            return fileLocation;
        }

        public static string UploadFile1(HttpPostedFileBase file, string FilePath, string fileName, string category, bool saveIcon)
        {
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            string fileLocation = Path.Combine(FilePath, fileName);
            file.SaveAs(fileLocation);
            if (saveIcon)
            {
                SaveImageAsIcon1(file, FilePath, fileName);
            }
            return fileLocation;
        }

        public static string GetRandomAlphaNumeric()
        {
            return Path.GetRandomFileName().Replace(".", "").Substring(0, 8).ToString();
        }

        public static string ModifiyFileName(string originalName)
        {
            string random = GetRandomAlphaNumeric();
            string[] imageNameArray = originalName.Split('.');
            return imageNameArray[0] + "_" + random + "." + imageNameArray[1];
        }

        public static void DeleteFiles(List<string> files, string path)
        {
            try
            {
                foreach (var fileName in files)
                {
                    DeleteFile(fileName, path);
                }
            }
            catch
            {
            }
        }

        public static string GenerateHash(string value, string salt)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(salt + value);
            data = System.Security.Cryptography.MD5.Create().ComputeHash(data);
            return Convert.ToBase64String(data);
        }

        public static void DeleteFile(string fileName, string path)
        {
            string fileLocation = Path.Combine(path, fileName);
            if (System.IO.File.Exists(fileLocation))
            {
                System.IO.File.Delete(fileLocation);
            }

            if (System.IO.File.Exists(fileLocation.Replace(fileName, "Icon\\" + fileName)))
            {
                System.IO.File.Delete(fileLocation.Replace(fileName, "Icon\\" + fileName));
            }
        }

        public static void SaveImageAsIcon(HttpPostedFile file, string path, string imageName)
        {
            using (var image = Image.FromFile(path + "\\" + imageName))
            using (var newImage = ScaleImage(image, 65, 65))
            {
                var FilePath = path + "\\Icon\\";
                if (!Directory.Exists(FilePath))
                {
                    Directory.CreateDirectory(FilePath);
                }
                newImage.Save(FilePath + imageName, ImageFormat.Png);
            }
        }

        public static void SaveImageAsIcon1(HttpPostedFileBase file, string path, string imageName)
        {
            using (var image = Image.FromFile(path + "\\" + imageName))
            using (var newImage = ScaleImage(image, 65, 65))
            {
                var FilePath = path + "\\Icon\\";
                if (!Directory.Exists(FilePath))
                {
                    Directory.CreateDirectory(FilePath);
                }
                newImage.Save(FilePath + imageName, ImageFormat.Png);
            }
        }

        public static Image ScaleImage(Image image, int width, int height)
        {
            var newImage = new Bitmap(width, height);

            using (var graphics = Graphics.FromImage(newImage))
                graphics.DrawImage(image, 0, 0, width, height);

            return newImage;
        }

        public static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["ProjectPoolDbEnities"].ToString();
        }

        //public static bool HasPermission(int userId)
        //{
        //    ProjectPoolDbEnities entity = new ProjectPoolDbEnities();
        //    var user = entity.UserRoles.Where(x => x.Role.Description == "SuperAdmin" && x.UserId == userId).ToList();
        //    if (user.Count > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

        //public static string GetProjectName(int projectId)
        //{
        //    ProjectServices projectService = new ProjectServices();
        //    return projectService.GetAll().Where(x => x.projectId == projectId).Select(x => x.projectName).SingleOrDefault();
        //}

        //public static string GetProjectGUID(int projectId)
        //{
        //    ProjectServices projectService = new ProjectServices();
        //    return projectService.GetAll().Where(x => x.projectId == projectId).Select(x => x.GUID).SingleOrDefault();
        //}

        //public static string GetTagGUID(int tagId)
        //{
        //    TagServices tagService = new TagServices();
        //    return tagService.GetAll().Where(x => x.TagId == tagId).Select(x => x.Guid).FirstOrDefault();
        //}

        //public static string GetArticleGUID(int articleId)
        //{
        //    ArticleServices articleService = new ArticleServices();
        //    return articleService.GetAll().Where(x => x.ArticleId == articleId).Select(x => x.GUID).SingleOrDefault();
        //}

        //public static int GetCategoryId(string categoryName)
        //{
        //    ProjectPoolDBEntities context = new ProjectPoolDBEntities();
        //    return context.Categories.Where(x => x.Name == categoryName).Select(x => x.CategoryId).FirstOrDefault();
        //}

        //public static string ConvertToString(int[] array)
        //{
        //    string str = "";
        //    for (int i = 0; i < array.Length; i++)
        //    {
        //        str = str + "," + array[i];
        //    }

        //    if (str != "")
        //    {
        //        str = str.Substring(1);
        //    }

        //    return str;
        //}

        //public static List<LatestViewModel> GetLatestPosts()
        //{
        //    ProjectPoolDBEntities context = new ProjectPoolDBEntities();
        //    ProjectServices projectService = new ProjectServices();
        //    ArticleServices articleService = new ArticleServices();

        //    var query = (from q in context.DataSetItems
        //                 where q.DataSet.Description == "LatestPosts" && q.Attribute1 == "Data" && q.DateDeleted == null
        //                 select new LatestViewModel
        //                 {
        //                     DataSetItemId = q.DataSetItemId,
        //                     DataSetId = q.DataSetId,
        //                     Category = q.Attribute2,
        //                     Id = q.Attribute3,
        //                     Guid = q.Attribute4,
        //                     //Name = (Convert.ToInt16(q.Attribute2) == 1) ? projectService.GetProjectById(Convert.ToInt16(q.Attribute3)).projectName : articleService.GetArticle(Convert.ToInt16(q.Attribute3)).Topic
        //                 }).OrderByDescending(x => x.DataSetItemId).Take(5).ToList();
        //    return query;
        //}
    }
}