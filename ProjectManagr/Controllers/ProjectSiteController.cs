using DataTables.Mvc;
using Models;
using Models.DTO;
using ProjectManagr.Cache;
using ProjectManagr.ViewModels;
using ServiceInterfaces;
using System.Collections.Generic;
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
        , IProjectService projectService)
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

            //Initialize the dropdown helper
            _dropdownHelper = new DropdownHelper(
                _applicationTypeService
                , _countryService
                , _departmentService
                , _siteItmFeedbackService
                , _siteService
                , _subportfolioService
                , _entityStatusService
                , _projectService);
        }

        // GET: ProjectSite
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get([ModelBinder(typeof(DataTablesBinder))] IDataTablesRequest requestModel)
        {
            FilterRequestDTO serviceRequest = this.CreateServiceRequest(requestModel);

            FilterResponseDTO<ProjectSite> serviceReponse = _projectSiteService.GetWithFilter(serviceRequest);

            List<ProjectSiteVM> data = new List<ProjectSiteVM>();
            serviceReponse.Data.ForEach(x => data.Add(new ProjectSiteVM(x)));

            return Json(new DataTablesResponse(requestModel.Draw, data, serviceReponse.FilteredCount, serviceReponse.TotalCount), JsonRequestBehavior.AllowGet);
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

                return Content("success");
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

                return Content("success");
            }
            catch
            {
                ModelState.AddModelError("", "Unable to update the Project Site");
                this.PopulateDropdowns(obj);
                return View("_CreateOrEdit", obj);
            }
        }

        // GET: ProjectSite/Delete/5
        public ActionResult Delete(int id)
        {
            ProjectSite detail = _projectSiteService.Get(id);
            ProjectSiteVM vm = new ProjectSiteVM(detail);
            return PartialView("_Delete", vm);
        }

        // POST: ProjectSite/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteProjectSite(int id)
        {
            try
            {
                string deletedby = System.Web.HttpContext.Current.User.Identity.Name;
                _projectSiteService.SoftDelete(id, deletedby);
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

                JsonResult result = new JsonResult
                {
                    Data = new { status = "success", project = obj },
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
            projectsite.CountryId = obj.CountryId;
            projectsite.SiteId = obj.SiteId;
            projectsite.SiteItmFeedbackId = obj.SiteItmFeedbackId;
            projectsite.DepartmentId = obj.DepartmentId;
            projectsite.ApplicationTypeId = obj.ApplicationTypeId;
            projectsite.Apex = obj.Apex;
            projectsite.PotentialValue = obj.PotentialValue;
            projectsite.SiteItm = obj.SiteItm;
            projectsite.SiteEngagementStart = obj.SiteEngagementStart;
            projectsite.SiteEngagementEnd = obj.SiteEngagementEnd;
            projectsite.HasBusinessImpact = obj.HasBusinessImpact; //Y or N : Yes or No -- ~Handle in C#
            projectsite.CommentsAndIssues = obj.CommentsAndIssues;
            projectsite.IsResourceRequired = obj.IsResourceRequired; //Y or N : Yes or No  -- ~Handle in c#
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

            //Add a new Project if Id is 0
            if(projectsite.ProjectId == 0)
            {
                projectsite.Project = new Project();
                projectsite.Project.Code = obj.Code;
                projectsite.Project.Name = obj.Name;
                projectsite.Project.Description = obj.Description;
                projectsite.Project.PmName = obj.PmName;
                projectsite.Project.ApplicationName = obj.ApplicationName;
                projectsite.Project.SubPortfolioId = obj.SubPortfolioIdRef;
                projectsite.CreatedBy = System.Web.HttpContext.Current.User.Identity.Name;
                projectsite.ModifiedBy = System.Web.HttpContext.Current.User.Identity.Name;

            }

            return projectsite;
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
            data.PmName = obj.PmName;
            data.ApplicationName = obj.ApplicationName;
            data.SubPortfolioId = obj.SubPortfolioId;

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
        }

        private void PopulateDropdowns(ProjectVM vm)
        {
            vm.SubPortfolios = _dropdownHelper.SubPortfolios;
        }
        #endregion Helpers
    }
}