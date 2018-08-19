using Models;
using Models.DTO;
using ProjectManagr.Cache;
using ProjectManagr.ViewModels;
using ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace ProjectManagr.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProjectSiteService _projectSiteService;
        private readonly ISiteService _siteService;
        private readonly DropdownHelper _dropdownHelper;

        public HomeController(IProjectSiteService projectSiteService, ISiteService siteService)
        {
            _projectSiteService = projectSiteService;
            _siteService = siteService;

            //Initialize the dropdown helper
            _dropdownHelper = new DropdownHelper(
                  null
                , null
                , null
                , null
                , _siteService
                , null
                , null
                , null);
        }
        public ActionResult Index(DashboardVM request)
        {
            //todo: Check with Spoorthi on logic
            //Should it filter by the entries that start after this date?
            //Should it filter if the records lie in this range
            DashboardVM vm = new DashboardVM();
            vm.Sites = _dropdownHelper.Sites;
            return View(vm);
        }

        public ActionResult Get(DashboardVM request)
        {
            //if(!ModelState.IsValid)
            //{
            //    return View("Index", request);
            //}
            DashboardVM vm = new DashboardVM();
            vm.StartDate = request.StartDate;
            vm.EndDate = request.EndDate;
            vm.Sites = _dropdownHelper.Sites;
            vm.SelectedSite = request.SelectedSite;

            //Get data based on filter applied
            Expression<Func<ProjectSite, bool>> filter = x => ((x.SiteEngagementStart >= vm.StartDate && x.SiteEngagementStart <= vm.EndDate)
                                            || (x.SiteEngagementEnd >= vm.StartDate && x.SiteEngagementEnd <= vm.EndDate)
                                            || (x.SiteEngagementStart <= vm.StartDate && x.SiteEngagementEnd >= vm.EndDate))
                                            && x.SiteId == vm.SelectedSite;
            List<ProjectSite> projectSites = _projectSiteService.GetWithFilter(filter);

            List<ProjectSiteVM> chartsData = new List<ProjectSiteVM>();
            projectSites.ForEach(x => chartsData.Add(new ProjectSiteVM(x)));

            vm.ChartData = chartsData;
            return new JsonResult { Data = vm, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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