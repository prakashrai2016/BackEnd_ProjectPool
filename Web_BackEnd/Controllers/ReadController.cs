using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectPool.DataEntity;
using ProjectPool.Repository;
using ProjectPool.Services;
using ProjectPool.Library;

namespace ProjectPool.Controllers
{
    public class ReadController : Controller
    {
        //
        // GET: /Read/

        public ActionResult Index()
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            Session["ActiveMenu"] = "manageRead";
            var readList = new ReadService(Common.GetConnectionString()).GetAll();
            ViewBag.SearchText = "";
            ViewBag.PageNumber = "1";
            ViewBag.PageSize = "10";
            ViewBag.OrderColumn = "";
            ViewBag.OrderBy = "";
            return View(readList);
        }

        public ActionResult New()
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult New(Read read)
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            var success = new ReadService(Common.GetConnectionString()).Save(read, false);
            if (success)
            {

            }
            return RedirectToAction("Detail", new { id = read.GUID });
        }

        public ActionResult Detail(string id)
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            var read = new ReadService(Common.GetConnectionString()).GetAll().Where(x => x.GUID == id).SingleOrDefault();
            return View(read);
        }


        [HttpPost]
        public ActionResult Search()
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            var searchString = Request.Form["SearchString"];
            var pageNumber = Convert.ToInt16(Request.Form["PageNumber"]);
            var pageSize = Convert.ToInt16(Request.Form["PageSize"]);
            var readList = new ReadService(Common.GetConnectionString()).GetAll().Where(x => x.Topic.Contains(searchString)).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            //var articleList = new ArticleService(Common.GetConnectionString()).LoadArticles("Title", "ASC", pageNumber, pageSize);

            ViewBag.SearchText = searchString;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            return View("_Partial", readList);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Read read)
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            var success = new ReadService(Common.GetConnectionString()).Save(read, true);

            return RedirectToAction("Detail", new { id = read.GUID });
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
                new ReadService(Common.GetConnectionString()).Delete(Convert.ToInt32(ids[i]));
            }

            var searchString = Request.Form["SearchString"];
            var pageNumber = Convert.ToInt16(Request.Form["PageNumber"]);
            var pageSize = Convert.ToInt16(Request.Form["PageSize"]);
            var readList = new ReadService(Common.GetConnectionString()).GetAll().Where(x => x.Topic.Contains(searchString)).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            //var articleList = new ArticleService(Common.GetConnectionString()).LoadArticles("Title", "ASC", pageNumber, pageSize);

            ViewBag.SearchText = searchString;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            return View("_Partial", readList);
        }

    }
}
