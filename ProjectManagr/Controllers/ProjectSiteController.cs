using DataTables.Mvc;
using Models;
using Models.DTO;
using ProjectManagr.Cache;
using ProjectManagr.ViewModels;
using ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace ProjectManagr.Controllers
{
    [RoutePrefix("MYDP")]
    public class ProjectSiteController : BaseController
    {
        private readonly IApplicationTypeService _applicationTypeService;
        private readonly ICountryService _countryService;
        private readonly IDepartmentService _departmentService;
        private readonly ISiteItmFeedbackService _siteItmFeedbackService;
        private readonly ISiteService _siteService;
        private readonly ISubPortfolioService _subportfolioService;
        private readonly IProjectSiteService _projectSiteService;
        private readonly IEntityStatusService _entityStatusService;
        private readonly IProjectService _projectService;
        private readonly IManagerService _managerService;
        private readonly DropdownHelper _dropdownHelper;

        public ProjectSiteController(
        IApplicationTypeService applicationTypeService
        , ICountryService countryService
        , IDepartmentService departmentService
        , ISiteItmFeedbackService siteItmFeedbackService
        , ISiteService siteService
        , ISubPortfolioService subportfolioService
        , IProjectSiteService projectSiteService
        , IEntityStatusService entityStatusService
        , IProjectService projectService
        , IManagerService managerService)
        {
            _applicationTypeService = applicationTypeService;
            _countryService = countryService;
            _departmentService = departmentService;
            _entityStatusService = entityStatusService;
            _projectSiteService = projectSiteService;
            _projectService = projectService;
            _siteItmFeedbackService = siteItmFeedbackService;
            _siteService = siteService;
            _subportfolioService = subportfolioService;
            _managerService = managerService;

            //Initialize the dropdown helper
            _dropdownHelper = new DropdownHelper(
                _applicationTypeService
                , _countryService
                , _departmentService
                , _siteItmFeedbackService
                , _siteService
                , _subportfolioService
                , _entityStatusService
                , _projectService
                , _managerService);
        }

        // GET: ProjectSite
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get()
        {

            Stopwatch sw = Stopwatch.StartNew();
            List<ProjectSite> serviceReponse = _projectSiteService.GetAll();
            sw.Stop();
            var part1 = sw.ElapsedMilliseconds;

            sw = Stopwatch.StartNew();
            List<ProjectSiteVM> vmList = new List<ProjectSiteVM>();
            serviceReponse.ForEach(x => vmList.Add(new ProjectSiteVM(x)));
            sw.Stop();
            var part2 = sw.ElapsedMilliseconds;

            JsonResult result = new JsonResult
            {
                Data = new { data = vmList, time1 = part1, time2 = part2 },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = int.MaxValue
            };

            return result;
        }

        // GET: ProjectSite/Details/5
        public ActionResult Details(int id)
        {
            ProjectSite detail = _projectSiteService.Get(id);
            ProjectSiteVM vm = new ProjectSiteVM(detail);
            return PartialView("_Details", vm);
        }

        // GET: ProjectSite/Create
        public ActionResult Create()
        {
            ProjectSiteVM vm = new ProjectSiteVM();
            this.PopulateDropdowns(vm);
            return View("_CreateOrEdit", vm);
        }

        // POST: ProjectSite/Create
        [HttpPost]
        public ActionResult Create(ProjectSiteVM obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    this.PopulateDropdowns(obj);
                    return PartialView("_CreateOrEdit", obj);
                }

                ProjectSite projectSite = TransferData(obj);

                _projectSiteService.Insert(projectSite);

                JsonResult result = new JsonResult
                {
                    Data = new { status = "success" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

                return result;
            }
            catch
            {
                ModelState.AddModelError("", "Unable to add the Project Site");
                return View("_CreateOrEdit", obj);
            }
        }

        // GET: ProjectSite/Edit/5
        public ActionResult Edit(int id)
        {
            ProjectSite detail = _projectSiteService.Get(id);
            ProjectSiteVM vm = new ProjectSiteVM(detail);
            vm.IsEdit = true;
            this.PopulateDropdowns(vm);
            return View("_CreateOrEdit", vm); ;
        }

        // POST: ProjectSite/Edit/5
        [HttpPost]
        public ActionResult Edit(ProjectSiteVM obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    this.PopulateDropdowns(obj);
                    return PartialView("_CreateOrEdit", obj);
                }

                ProjectSite projectSite = TransferData(obj);

                _projectSiteService.Update(projectSite);

                JsonResult result = new JsonResult
                {
                    Data = new { status = "success" },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                return result;
            }
            catch
            {
                ModelState.AddModelError("", "Unable to update the Project Site");
                this.PopulateDropdowns(obj);
                return View("_CreateOrEdit", obj);
            }
        }

        // GET: ProjectSite/Delete/5
        public ActionResult DeleteWarning(int count)
        {
            ViewBag.Count = count;
            return PartialView("_Delete");
        }

        // POST: ProjectSite/Delete/5
        [HttpPost]
        public ActionResult Delete(List<int> ids)
        {
            try
            {
                string deletedby = System.Web.HttpContext.Current.User.Identity.Name;
                _projectSiteService.SoftDelete(ids, deletedby);
                return Content("success");
            }
            catch
            {
                ModelState.AddModelError("", "Unable to Delete the Project Site");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Delete", new ProjectSiteVM()); //todo: Find how to retain the data to show
            }
        }

        // GET: ProjectSite/AddProject
        public ActionResult AddProject()
        {
            ProjectVM vm = new ProjectVM();
            this.PopulateDropdowns(vm);
            return View("_AddProject", vm);
        }

        [HttpPost]
        public ActionResult AddProject(ProjectVM obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    this.PopulateDropdowns(obj);
                    return PartialView("_AddProject", obj);
                }

                Project data = TransferData(obj);
                _projectService.Insert(data);

                JsonResult result = new JsonResult
                {
                    Data = new { status = "success", project = new ProjectVM(data) },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                return result;
            }
            catch
            {
                ModelState.AddModelError("", "Unable to add the Project");
                this.PopulateDropdowns(obj);
                return View("_AddProject", obj);
            }
        }

        public ActionResult GetProject(int id)
        {
            Project detail = _projectService.Get(id);
            ProjectVM vm = new ProjectVM(detail);

            return new JsonResult
            {
                Data = new { project = vm },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        #region Helpers

        private ProjectSite TransferData(ProjectSiteVM obj)
        {
            if (obj == null)
            {
                return null;
            }

            ProjectSite projectsite = new ProjectSite();
            projectsite.Id = obj.Id;
            projectsite.ProjectId = obj.ProjectId;
            projectsite.EntityStatusId = obj.EntityStatusId;
            projectsite.SiteId = obj.SiteId;
            projectsite.SiteItmId = obj.SiteItmId;
            projectsite.SiteItmFeedbackId = obj.SiteItmFeedbackId;
            projectsite.DepartmentId = obj.DepartmentId;
            projectsite.ApplicationTypeId = obj.ApplicationTypeId;
            projectsite.Apex = obj.Apex;
            projectsite.PotentialValue = obj.PotentialValue;
            projectsite.SiteEngagementStart = obj.SiteEngagementStart ?? DateTime.MinValue;
            projectsite.SiteEngagementEnd = obj.SiteEngagementEnd ?? DateTime.MinValue;
            projectsite.HasBusinessImpact = obj.HasBusinessImpact;
            projectsite.CommentsAndIssues = obj.CommentsAndIssues;
            projectsite.IsResourceRequired = obj.IsResourceRequired;
            projectsite.Attachment = obj.Attachment;
            projectsite.CreatedBy = obj.CreatedBy;
            projectsite.ModifiedBy = obj.ModifiedBy;
            projectsite.IsActive = obj.IsActive;

            if (string.IsNullOrEmpty(projectsite.CreatedBy) && string.IsNullOrEmpty(projectsite.CreatedBy))
            {
                //In case of a new record
                projectsite.CreatedBy = System.Web.HttpContext.Current.User.Identity.Name;
                projectsite.ModifiedBy = System.Web.HttpContext.Current.User.Identity.Name;
            }
            else
            {
                //In case of a edited/deleted record
                projectsite.ModifiedBy = System.Web.HttpContext.Current.User.Identity.Name;
            }
            projectsite.CreatedDate = obj.CreatedDate;
            projectsite.ModifiedDate = obj.ModifiedDate;

            return projectsite;
        }

        public ActionResult TestError()
        {
            throw new Exception();
        }


        private Project TransferData(ProjectVM obj)
        {
            if (obj == null)
            {
                return null;
            }

            Project data = new Project();
            data.Code = obj.Code;
            data.Name = obj.Name;
            data.Description = obj.Description;
            data.ApplicationName = obj.ApplicationName;
            data.SubPortfolioId = obj.SubPortfolioId;
            data.PmId = obj.PmId;
            return data;
        }

        private void PopulateDropdowns(ProjectSiteVM vm)
        {
            vm.EntityStatuses = _dropdownHelper.EntityStatuses;
            vm.Projects = _dropdownHelper.Projects;
            vm.ApplicationTypes = _dropdownHelper.ApplicationTypes;
            vm.Countries = _dropdownHelper.Countries;
            vm.Departments = _dropdownHelper.Departments;
            vm.SiteItmFeedbacks = _dropdownHelper.SiteItmFeedbacks;
            vm.Sites = _dropdownHelper.Sites;
            vm.SubPortfolios = _dropdownHelper.SubPortfolios;
            vm.CountrySiteMap = _dropdownHelper.CountrySiteMap;
            vm.Managers = _dropdownHelper.Managers;
        }

        private void PopulateDropdowns(ProjectVM vm)
        {
            vm.SubPortfolios = _dropdownHelper.SubPortfolios;
            vm.Pms = _dropdownHelper.Managers;
        }
        #endregion Helpers
    }
}