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
            vm.EntityStatuses = _dropdownHelper.EntityStatuses;
            return View(vm);
        }

        public ActionResult Get(DashboardVM request)
        {
            List<ProjectSite> projectSites = _projectSiteService.GetWithFilter(request.StartDate, request.EndDate, request.SelectedSite, request.EntityStatusId);

            int inProgressConfirmed = _entityStatusService.GetAll().FirstOrDefault(x => x.Name == Constants.EntityStatus.InProgressConfirmed).Id;

            List<ProjectSiteVM> chartsData = new List<ProjectSiteVM>();

            //We assign IsBeyondToday as true, if the status is 'In Progress-Confirmed' and the End date is beyond today
            projectSites.ForEach(x => {
                ProjectSiteVM vm = new ProjectSiteVM(x);
                vm.IsProgressBeyondToday = vm.SiteEngagementEnd > DateTime.Today && vm.EntityStatusId == inProgressConfirmed;
                chartsData.Add(vm);
                });

            //We also sort the data in the order such that the records with 'IsBeyondToday' come at the top
            DashboardVM response = new DashboardVM();
            response.StartDate = request.StartDate;
            response.EndDate = request.EndDate;
            response.Sites = _dropdownHelper.Sites;
            response.SelectedSite = request.SelectedSite;
            response.EntityStatusId = request.EntityStatusId;
            response.ChartData = chartsData.OrderBy(x => x.IsProgressBeyondToday ? 0 : 1).ToList();
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