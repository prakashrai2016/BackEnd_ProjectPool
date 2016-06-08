using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectPool.Library;

namespace Web_BackEnd.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/

        public ActionResult Index()
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            return View();
        }

        [HttpGet]
        public string SetActiveMenu(string menuId)
        {
            Session["ActiveMenu"] = menuId;
            return "Set";
        }

    }
}
