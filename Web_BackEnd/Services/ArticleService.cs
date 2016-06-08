using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ProjectPool.Models;
using ProjectPool.Library;
using ProjectPool.DataEntity;
using ProjectPool.Repository;
using System.IO;

namespace ProjectPool.Services
{
    public class ArticleService : GenericRepository<Article>
    {
        string ConnectionString;

        public ArticleService(string connectionString)
            : base(connectionString)
        {
            ConnectionString = connectionString;
        }

        public IEnumerable<Article> GetAll()
        {
            return this.Load();
        }

        public IEnumerable<Article> LoadArticles(string column, string orderBy, int page, int pageSize)
        {
            return this.Load(x => x.DateDeleted == null, column, orderBy, page, pageSize);
        }

        public Article GetById(int articleId)
        {
            return Load(x => x.ArticleId == articleId).FirstOrDefault();
        }

        public Article GetByGuid(string guid)
        {
            return Load(x => x.GUID == guid).FirstOrDefault();
        }

        public int Save(Article article, bool isEdit)
        {
            try {
                var request = HttpContext.Current.Request;
                var articleImage = request.Files["uploadArticleImage"] as HttpPostedFile;

                if (articleImage != null && articleImage.FileName != "")
                {
                    

                    string fileName = article.Title + "_" + DateTime.Now.ToString("yyMMdd") + "." + articleImage.FileName.Split('.')[1];
                    fileName = fileName.Replace(" ", "_");

                    string path = Common.GetAppSettingValue("ArticleFilePath");
                    path = Path.Combine(HttpContext.Current.Server.MapPath(path), article.Title.Replace(' ', '_'));

                    if (isEdit)
                    {
                        Common.DeleteFile(article.ImageName, path);
                    }

                    Common.UploadFile(articleImage, path, fileName, "Article", true);
                    article.ImageName = fileName;
                }
                

                if (isEdit)
                {
                    article.DateModified = DateTime.Now;
                    Update(article);
                        
                }
                else
                {
                    article.GUID = Guid.NewGuid().ToString();
                    article.DateCreated = DateTime.Now;
                    Add(article);
                }
                

                SaveChanges();

                int categoryId = new CategoryService(ConnectionString).GetCateGoryId("Article");
                string articleTagLinks = request.Form["ArticleTags"].ToString();
                new TagLinkService(ConnectionString).AddTagLinks(articleTagLinks, categoryId, article.ArticleId);
                return article.ArticleId;
            }
            catch { return 0; }
        }

        public string UploadTempImage()
        {
            var request = HttpContext.Current.Request;
            var articleImage = request.Files["upload"] as HttpPostedFile;

            string fileName = articleImage.FileName.Split('.')[0] + "_" + DateTime.Now.ToString("yyMMdd") + "." + articleImage.FileName.Split('.')[1];
            fileName = fileName.Replace(" ", "_");

            string path = Common.GetAppSettingValue("ArticleFilePath");
            path = Path.Combine(HttpContext.Current.Server.MapPath(path), "Temp");
            
            Common.UploadFile(articleImage, path, fileName, "Article", false);

            return Common.GetAppSettingValue("BaseUrl") + "Data//Article//Temp//" + fileName;
        }

        public void Delete(int articleId)
        {
            var article = Load(x => x.ArticleId == articleId).FirstOrDefault();
            string path = Common.GetAppSettingValue("ArticleFilePath");
            path = Path.Combine(HttpContext.Current.Server.MapPath(path), article.Title.Replace(' ', '_'));
            Common.DeleteFile(article.ImageName, path);
            Remove(article);
            SaveChanges();
        }
    }
}