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
        private readonly IManagerService _managerService;

        public DropdownHelper(
             IApplicationTypeService applicationTypeService
        , ICountryService countryService
        , IDepartmentService departmentService
        , ISiteItmFeedbackService siteItmFeedbackService
        , ISiteService siteService
        , ISubPortfolioService subportfolioService
        , IEntityStatusService entityStatusService
        , IProjectService projectService
        , IManagerService managerService)
        {
            _applicationTypeService = applicationTypeService;
            _countryService = countryService;
            _departmentService = departmentService;
            _entityStatusService = entityStatusService;
            _projectService = projectService;
            _siteItmFeedbackService = siteItmFeedbackService;
            _siteService = siteService;
            _subportfolioService = subportfolioService;
            _managerService = managerService;
        }

        public List<SelectListItem> EntityStatuses
        {
            get
            {
                List<EntityStatus> allEntityStatus = _entityStatusService.GetAll().OrderBy(x=>x.Name).ToList();
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
                List<Project> allProjects = _projectService.GetAll().OrderBy(x => x.Name).ToList();
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
                List<ApplicationType> items = _applicationTypeService.GetAll().OrderBy(x => x.Name).ToList();
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
                List<Country> items = _countryService.GetCountriesWithSites().OrderBy(x=>x.Name).ToList();
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
                List<Department> items = _departmentService.GetAll().OrderBy(x => x.Name).ToList();
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
                List<SiteItmFeedback> items = _siteItmFeedbackService.GetAll().OrderBy(x => x.Name).ToList();
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
                List<Site> items = _siteService.GetAll().OrderBy(x => x.Name).ToList();
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
                List<SubPortfolio> items = _subportfolioService.GetAll().OrderBy(x => x.Name).ToList();
                List<SelectListItem> dropdown = items
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();

                return dropdown;
            }
        }
        public List<SelectListItem> Managers
        {
            get
            {
                List<Manager> items = _managerService.GetAll().OrderBy(x => x.Name).ToList();
                List<SelectListItem> dropdown = items
                    .Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();

                return dropdown;
            }
        }


        public Dictionary<int, List<int>> CountrySiteMap
        {
            get
            {
                Dictionary<int, List<int>> returnVal = new Dictionary<int, List<int>>();
                List<Country> countries = _countryService.GetCountriesWithSites();

                foreach(Country country in countries)
                {
                    List<int> siteIds = country.Site.Select(x => x.Id).ToList() ?? new List<int>();
                    returnVal.Add(country.Id, siteIds);
                }
                return returnVal;
            }
        }
    }
}