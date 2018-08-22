using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManagr.ViewModels
{
    public class DashboardVM
    {
        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set;}

        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Site")]
        public int SelectedSite { get; set; }

        [Display(Name = "Entity Status")]
        public int? EntityStatusId { get; set; }

        public string MinDate { get; set; }

        public string MaxDate { get; set; }

        public List<ProjectSiteVM> ChartData { get; set; }
        #region Dropdowns
        public List<SelectListItem> Sites { get; set; }

        public List<SelectListItem> EntityStatuses { get; set; }
        #endregion
    }
}