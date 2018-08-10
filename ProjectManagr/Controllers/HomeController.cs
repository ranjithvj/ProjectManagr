using ProjectManagr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManagr.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get()
        {
            List<ProjectVM> data = new List<ProjectVM>();
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

            for (int i = 0; i < 10; i++)
            {
                ProjectVM project = new ProjectVM();
                //project.ProjectName = "Project " + i;
                //project.SiteEngagementStart = DateTime.Today.AddMonths(-i);
                //project.SiteEngagementEnd = DateTime.Today.AddMonths(i);
                //project.Site = "Buenos Aires (BAR)";
                //project.EntityStatus = "S&I";
                //project.EntityStatusColor = colors[i];
                data.Add(project);
            }

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