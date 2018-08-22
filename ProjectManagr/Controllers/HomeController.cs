using Models;
using Models.DTO;
using Models.Shared;
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
        private readonly IEntityStatusService _entityStatusService;
        private readonly DropdownHelper _dropdownHelper;

        public HomeController(IProjectSiteService projectSiteService, ISiteService siteService
            , IEntityStatusService entityStatusService)
        {
            _projectSiteService = projectSiteService;
            _siteService = siteService;
            _entityStatusService = entityStatusService;
            //Initialize the dropdown helper
            _dropdownHelper = new DropdownHelper(
                  null
                , null
                , null
                , null
                , _siteService
                , null
                , _entityStatusService
                , null);
        }
        public ActionResult Index(DashboardVM request)
        {
            //todo: Check with Spoorthi on logic
            //Should it filter by the entries that start after this date?
            //Should it filter if the records lie in this range
            DashboardVM vm = new DashboardVM();
            vm.Sites = _dropdownHelper.Sites;
            vm.EntityStatuses = _dropdownHelper.EntityStatuses;
            return View(vm);
        }

        public ActionResult Get(DashboardVM request)
        {
            List<ProjectSite> projectSites = _projectSiteService.GetWithFilter(request.StartDate, request.EndDate, request.SelectedSite, request.EntityStatusId);

            List<ProjectSiteVM> chartsData = new List<ProjectSiteVM>();
            projectSites.ForEach(x => chartsData.Add(new ProjectSiteVM(x)));

            DashboardVM response = new DashboardVM();
            response.StartDate = request.StartDate;
            response.EndDate = request.EndDate;
            response.Sites = _dropdownHelper.Sites;
            response.SelectedSite = request.SelectedSite;
            response.EntityStatusId = request.EntityStatusId;
            response.ChartData = chartsData;
            response.MinDate = chartsData?.Select(x => x.SiteEngagementStart).OrderBy(x => x).FirstOrDefault().ToString()
                ?? DateTime.MinValue.ToString();
            response.MaxDate = chartsData?.Select(x => x.SiteEngagementEnd).OrderByDescending(x => x).FirstOrDefault().ToString()
                ?? DateTime.MaxValue.ToString();

            return new JsonResult
            {
                Data = new { data = response },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue
            };
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