using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectPool.Services;
using ProjectPool.Library;
using ProjectPool.Models;
using ProjectPool.Repository;
using ProjectPool.DataEntity;

namespace ProjectPool.Controllers
{
    public class TagController : Controller
    {
        public ActionResult Index()
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            Session["ActiveMenu"] = "manageTag";
            var tagList = new TagService(Common.GetConnectionString()).GetAll();

            ViewBag.SearchText = "";
            ViewBag.PageNumber = "1";
            ViewBag.PageSize = "10";
            ViewBag.OrderColumn = "";
            ViewBag.OrderBy = "";
            return View(tagList);
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
            var TagList = new TagService(Common.GetConnectionString()).GetAll().Where(x => x.Name.Contains(searchString)).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            //var TagList = new TagService(Common.GetConnectionString()).LoadTags("Title", "ASC", pageNumber, pageSize);

            ViewBag.SearchText = searchString;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            return View("_Partial", TagList);
        }

        [HttpPost]
        public ActionResult NewTag()
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            Tag tag = new Tag() {
                Name = Request.Form["tagName"],
                Description = Request.Form["Description"]
            };


            try
            {
            //    var tagImage = Request.Files["uploadTagImage"] as HttpPostedFile;
                new TagService(Common.GetConnectionString()).Save(tag, false);
            
            }

            catch {
            
            }

            return RedirectToAction("Detail", "Article", new { id = Request.Form["articleGUID"] });
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
        public ActionResult New(Tag tag)
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            var success = new TagService(Common.GetConnectionString()).Save(tag, false);

            if (success)
            {

            }
            return RedirectToAction("Detail", new { id = tag.GUID });
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Tag tag)
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            var success = new TagService(Common.GetConnectionString()).Save(tag, true);

            if (success)
            {
                
            }
            return RedirectToAction("Detail", new { id = tag.GUID });
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
                new TagService(Common.GetConnectionString()).Delete(Convert.ToInt32(ids[i]));
            }

            var searchString = Request.Form["SearchString"];
            var pageNumber = Convert.ToInt16(Request.Form["PageNumber"]);
            var pageSize = Convert.ToInt16(Request.Form["PageSize"]);
            var TagList = new TagService(Common.GetConnectionString()).GetAll().Where(x => x.Name.Contains(searchString)).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            //var TagList = new TagService(Common.GetConnectionString()).LoadTags("Title", "ASC", pageNumber, pageSize);

            ViewBag.SearchText = searchString;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            return View("_Partial", TagList);
        }

        public ActionResult Detail(string id)
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            int categoryId = new CategoryService(Common.GetConnectionString()).GetCateGoryId("Tag");
            
            return View(new TagService(Common.GetConnectionString()).Load(x => x.GUID == id).SingleOrDefault());
        }
    }
}
