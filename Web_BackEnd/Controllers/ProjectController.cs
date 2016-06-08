using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectPool.Models;
using ProjectPool.Services;
using ProjectPool.DataEntity;
using ProjectPool.Library;

namespace ProjectPool.Controllers
{
    public class ProjectController : Controller
    {
        public ActionResult Index()
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            Session["ActiveMenu"] = "manageProject";
            ProjectService projectService = new ProjectService(Common.GetConnectionString());
            var projectList = projectService.GetAll().Take(10);
            ViewBag.SearchText = "";
            ViewBag.PageNumber = "1";
            ViewBag.PageSize = "10";
            ViewBag.OrderColumn = "";
            ViewBag.OrderBy = "";
            return View(projectList);
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
            var ProjectList = new ProjectService(Common.GetConnectionString()).GetAll().Where(x => x.Title.Contains(searchString)).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            //var ProjectList = new ProjectService(Common.GetConnectionString()).LoadProjects("Title", "ASC", pageNumber, pageSize);

            ViewBag.SearchText = searchString;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            return View("_Partial", ProjectList);
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
        public ActionResult New(Project project)
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            var projectId = new ProjectService(Common.GetConnectionString()).Save(project, false);

            if (projectId > 0)
            {
                
            }

            return RedirectToAction("Detail", new { id = project.GUID });
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
                new TagLinkService(Common.GetConnectionString()).DeleteLinks(Convert.ToInt32(ids[i]), 3);
                new ProjectStepService(Common.GetConnectionString()).Delete(Convert.ToInt32(ids[i]));
                new ProjectService(Common.GetConnectionString()).Delete(Convert.ToInt32(ids[i]));
            }

            var searchString = Request.Form["SearchString"];
            var pageNumber = Convert.ToInt16(Request.Form["PageNumber"]);
            var pageSize = Convert.ToInt16(Request.Form["PageSize"]);
            var ProjectList = new ProjectService(Common.GetConnectionString()).GetAll().Where(x => x.Title.Contains(searchString)).Skip((pageNumber - 1) * pageSize).Take(pageSize);
            //var ProjectList = new ProjectService(Common.GetConnectionString()).LoadProjects("Title", "ASC", pageNumber, pageSize);

            ViewBag.SearchText = searchString;
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            return View("_Partial", ProjectList);
        }

        public ActionResult Detail(string id)
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            int categoryId = new CategoryService(Common.GetConnectionString()).GetCateGoryId("Project");
            ViewBag.TagKeyValueList = new TagService(Common.GetConnectionString()).GetTagKeyValueList();

            var ProjectId = new ProjectService(Common.GetConnectionString()).Load(x => x.GUID == id).Select(x => x.ProjectId).First();

            ViewBag.LinkedTags = new TagLinkService(Common.GetConnectionString()).GetLinkedTags(ProjectId, categoryId);

            return View(new ProjectService(Common.GetConnectionString()).Load(x => x.GUID == id).SingleOrDefault());
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Project Project)
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            var ProjectId = new ProjectService(Common.GetConnectionString()).Save(Project, true);

            return RedirectToAction("Detail", new { id = Project.GUID });
        }

        [HttpGet]
        public ActionResult Steps(string id)
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            var project = new ProjectService(Common.GetConnectionString()).Load(x => x.GUID == id).FirstOrDefault();
            ViewBag.ProjectName = project.Title;
            ViewBag.ProjectId = project.ProjectId;
            ViewBag.ProjectGUID = id;

            var projectStepService = new ProjectStepService(Common.GetConnectionString());

            ViewBag.StepIds = projectStepService.Load(x => x.ProjectId == project.ProjectId).Select(x => x.ProjectStepId).ToList();

            ProjectStep firstStep = null; 
            if (Session["ActiveStep"] != null)
            {
                int projectStepId = Convert.ToInt16(Session["ActiveStep"]);
                firstStep = projectStepService.Load(x => x.ProjectId == project.ProjectId && x.ProjectStepId == projectStepId).FirstOrDefault();
            }

            if (firstStep == null)
            {
                firstStep = projectStepService.Load(x => x.ProjectId == project.ProjectId).FirstOrDefault();
            }
            
            return View(firstStep);
        }

        public ActionResult StepDetail(string id)
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            int projectId = Convert.ToInt16(id);
            var step = new ProjectStepService(Common.GetConnectionString()).Load(x => x.ProjectStepId == projectId).FirstOrDefault();

            var projectName = new ProjectService(Common.GetConnectionString()).Load(x => x.ProjectId == step.ProjectId).Select(x => x.Title).SingleOrDefault();
            ViewBag.ProjectName = projectName;
            

            return View("_StepDetail", step);
        }

        public ActionResult NewStep(string id)
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            var project = new ProjectService(Common.GetConnectionString()).Load(x => x.GUID == id).SingleOrDefault();
            ViewBag.ProjectName = project.Title;
            ViewBag.ProjectId = project.ProjectId;
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult NewStep(ProjectStep projectStep)
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            new ProjectStepService(Common.GetConnectionString()).Save(projectStep, false);
            var guid = new ProjectService(Common.GetConnectionString()).Load(x => x.ProjectId == projectStep.ProjectId).Select(x => x.GUID).FirstOrDefault();
            Session["ActiveStep"] = projectStep.ProjectStepId;
            return RedirectToAction("Steps", new { id = guid });
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditStep(ProjectStep projectStep)
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            new ProjectStepService(Common.GetConnectionString()).Save(projectStep, true);
            var guid = new ProjectService(Common.GetConnectionString()).Load(x => x.ProjectId == projectStep.ProjectId).Select(x => x.GUID).FirstOrDefault();

            Session["ActiveStep"] = projectStep.ProjectStepId;
            return RedirectToAction("Steps", new { id = guid});
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Steps(ProjectStepList stepList)
        {
            if (!Common.CheckUserSession())
            {
                return this.RedirectToAction("Index", "Login");
            }

            var success = new ProjectStepService(Common.GetConnectionString()).SaveSteps(stepList);

            if (success)
            {
                return RedirectToAction("Index");
            }
            return View(new { id = stepList.projectId});
        }

        public string UploadTempImage()
        {
            var filePath = new ProjectService(Common.GetConnectionString()).UploadTempImage();

            return "<script>window.parent.CKEDITOR.tools.callFunction(1, '" + filePath + "', 'Successfully added')</script>"; ;
        }

    }
}
