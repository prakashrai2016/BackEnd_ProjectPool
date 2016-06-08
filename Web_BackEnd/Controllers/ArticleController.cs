using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectPool.Services;
using ProjectPool.Library;
using ProjectPool.DataEntity;

namespace ProjectPool.Controllers
{
    public class ArticleController : Controller
    {
        //
        // GET: /Article/

        public ActionResult Index()
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }
            //var articleList = new ArticleService(Common.GetConnectionString()).LoadArticles("ArticleId", "ASC", 1, 10);

            var articleList = new ArticleService(Common.GetConnectionString()).GetAll().Take(10);

            ViewBag.SearchText = "";
            ViewBag.PageNumber = "1";
            ViewBag.PageSize = "10";
            ViewBag.OrderColumn = "";
            ViewBag.OrderBy = "";
            return View(articleList);
        }

        [HttpPost]
        public ActionResult Search()
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            Session["ActiveMenu"] = "manageArticle";
            var searchString = Request.Form["SearchString"];
            var pageNumber = Convert.ToInt16(Request.Form["PageNumber"]);
            var pageSize = Convert.ToInt16(Request.Form["PageSize"]);
            var articleList = new ArticleService(Common.GetConnectionString()).GetAll().Where(x => x.Title.Contains(searchString)).Skip((pageNumber - 1)*pageSize).Take(pageSize);
            //var articleList = new ArticleService(Common.GetConnectionString()).LoadArticles("Title", "ASC", pageNumber, pageSize);

            ViewBag.SearchText = searchString;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            return View("_Partial", articleList);
        }

        [HttpGet]
        public ActionResult New()
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            ViewBag.TagKeyValueList = new TagService(Common.GetConnectionString()).GetTagKeyValueList();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult New(Article article)
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            var articleId = new ArticleService(Common.GetConnectionString()).Save(article, false);
            
            if (articleId > 0)
            {
            }
            return RedirectToAction("Detail", new { id = article.GUID });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            int categoryId = new CategoryService(Common.GetConnectionString()).GetCateGoryId("Article");
            ViewBag.TagKeyValueList = new TagService(Common.GetConnectionString()).GetTagKeyValueList();

            var test = new TagLinkService(Common.GetConnectionString()).GetLinkedTags(id, categoryId);
            ViewBag.LinkedTags = new TagLinkService(Common.GetConnectionString()).GetLinkedTags(id, categoryId);

            return View(new ArticleService(Common.GetConnectionString()).Load(x => x.ArticleId == id).SingleOrDefault());
        }

        public ActionResult Detail(string id)
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            int categoryId = new CategoryService(Common.GetConnectionString()).GetCateGoryId("Article");
            ViewBag.TagKeyValueList = new TagService(Common.GetConnectionString()).GetTagKeyValueList();

            var articleId = new ArticleService(Common.GetConnectionString()).Load(x => x.GUID == id).Select(x => x.ArticleId).First();

            ViewBag.LinkedTags = new TagLinkService(Common.GetConnectionString()).GetLinkedTags(articleId, categoryId);

            return View(new ArticleService(Common.GetConnectionString()).Load(x => x.GUID == id).SingleOrDefault());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Article article)
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            var articleId = new ArticleService(Common.GetConnectionString()).Save(article, true);

            if (articleId > 0)
            {
                if (Convert.ToString(Request.Form["addReference"]) == "Y")
                {
                    ViewBag.articleId = articleId;
                    return View("AddReference");
                }
                return RedirectToAction("Detail", new { id = article.GUID});
            }

            ViewBag.TagKeyValueList = new TagService(Common.GetConnectionString()).GetTagKeyValueList();
            return View();
        }

        public ActionResult Delete(string id)
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            var ids = id.Split(',');
            for (int i = 0; i < ids.Length; i++)
            {
                new TagLinkService(Common.GetConnectionString()).DeleteLinks(Convert.ToInt32(ids[i]), 4);
                new ArticleService(Common.GetConnectionString()).Delete(Convert.ToInt32(ids[i]));
            }

            var searchString = Request.Form["SearchString"];
            var pageNumber = Convert.ToInt16(Request.Form["PageNumber"]);
            var pageSize = Convert.ToInt16(Request.Form["PageSize"]);
            var articleList = new ArticleService(Common.GetConnectionString()).GetAll().Where(x => x.Title.Contains(searchString)).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            //var articleList = new ArticleService(Common.GetConnectionString()).LoadArticles("Title", "ASC", pageNumber, pageSize);

            ViewBag.SearchText = searchString;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            return View("_Partial", articleList);
        }

        public string UploadTempImage()
        {
            var filePath = new ArticleService(Common.GetConnectionString()).UploadTempImage();

            return "<script>window.parent.CKEDITOR.tools.callFunction(1, '" + filePath + "', 'Successfully added')</script>";;
        }

    }
}
