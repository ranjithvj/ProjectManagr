using Models;
using ProjectManagr.ViewModels;
using ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManagr.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProjectSiteService _projectSiteService;

        public HomeController(IProjectSiteService projectSiteService)
        {
            _projectSiteService = projectSiteService;
        }
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get()
        {
            List<ProjectSite> data = _projectSiteService.GetAll();
            List<ProjectSiteVM> chartsData = new List<ProjectSiteVM>();
            data.ForEach(x => chartsData.Add(new ProjectSiteVM(x)));
            List<string> colors = new List<string> { "#F5B041"
                                  ,"#2ECC71"
                                  ,"#273746"
                                  ,"#1ABC9C"
                                  ,"#F1C40F"
                                  ,"#D35400"
                                  ,"#99A3A4"
                                  ,"#E74C3C"
                                  ,"#6C3483"
                                  , "#D8076C"};


            //data = data.Where(x => x.SiteEngagementStart >= DateTime.Parse(startDate) && x.SiteEngagementEnd <= DateTime.Parse(endDate)).ToList();

            return new JsonResult { Data = data, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult ReloadChart()
        {
            return PartialView("_Dashboard");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}