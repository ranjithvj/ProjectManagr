using Models;
using ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManagr.Cache
{
    public class DropdownHelper
    {
        private readonly IApplicationTypeService _applicationTypeService;
        private readonly ICountryService _countryService;
        private readonly IDepartmentService _departmentService;
        private readonly ISiteItmFeedbackService _siteItmFeedbackService;
        private readonly ISiteService _siteService;
        private readonly ISubPortfolioService _subportfolioService;
        private readonly IEntityStatusService _entityStatusService;
        private readonly IProjectService _projectService;

        public DropdownHelper(
             IApplicationTypeService applicationTypeService
        , ICountryService countryService
        , IDepartmentService departmentService
        , ISiteItmFeedbackService siteItmFeedbackService
        , ISiteService siteService
        , ISubPortfolioService subportfolioService
        , IEntityStatusService entityStatusService
        , IProjectService projectService)
        {
            _applicationTypeService = applicationTypeService;
            _countryService = countryService;
            _departmentService = departmentService;
            _entityStatusService = entityStatusService;
            _projectService = projectService;
            _siteItmFeedbackService = siteItmFeedbackService;
            _siteService = siteService;
            _subportfolioService = subportfolioService;
        }

        public List<SelectListItem> EntityStatuses
        {
            get
            {
                List<EntityStatus> allEntityStatus = new List<EntityStatus>();

                //if cache is present take from there
                //else
                allEntityStatus = _entityStatusService.GetAll();
                List<SelectListItem> dropdown = allEntityStatus
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList(); //todo: check how to convert the string back to int

                return dropdown;
            }
        }

        public List<SelectListItem> Projects
        {
            get
            {
                List<Project> allProjects = new List<Project>();

                allProjects = _projectService.GetAll();
                List<SelectListItem> dropdown = allProjects
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();

                return dropdown;
            }
        }

        public List<SelectListItem> ApplicationTypes
        {
            get
            {
                List<ApplicationType> items = new List<ApplicationType>();

                items = _applicationTypeService.GetAll();
                List<SelectListItem> dropdown = items
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();

                return dropdown;
            }
        }

        public List<SelectListItem> Countries
        {
            get
            {
                List<Country> items = new List<Country>();

                items = _countryService.GetAll();
                List<SelectListItem> dropdown = items
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();

                return dropdown;
            }
        }

        public List<SelectListItem> Departments
        {
            get
            {
                List<Department> items = new List<Department>();

                items = _departmentService.GetAll();
                List<SelectListItem> dropdown = items
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();

                return dropdown;
            }
        }

        public List<SelectListItem> SiteItmFeedbacks
        {
            get
            {
                List<SiteItmFeedback> items = new List<SiteItmFeedback>();

                items = _siteItmFeedbackService.GetAll();
                List<SelectListItem> dropdown = items
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();

                return dropdown;
            }
        }

        public List<SelectListItem> Sites
        {
            get
            {
                List<Site> items = new List<Site>();

                items = _siteService.GetAll();
                List<SelectListItem> dropdown = items
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();

                return dropdown;
            }
        }

        public List<SelectListItem> SubPortfolios
        {
            get
            {
                List<SubPortfolio> items = new List<SubPortfolio>();

                items = _subportfolioService.GetAll();
                List<SelectListItem> dropdown = items
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();

                return dropdown;
            }
        }
    }
}