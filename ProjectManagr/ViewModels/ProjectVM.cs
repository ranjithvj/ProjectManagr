using Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManagr.ViewModels
{
    public class ProjectVM : BaseVM
    {
        public ProjectVM()
        {

        }

        public ProjectVM(Project obj)
        {
            this.Id = obj.Id;
            this.Code = obj.Code;
            this.Name = obj.Name;
            this.Description = obj.Description;
            this.PmName = obj.PmName;
            this.ApplicationName = obj.ApplicationName;
            this.SubPortfolioId = obj.SubPortfolioId;
            this.SubPortfolioName = obj.SubPortfolio?.Name;
        }

        #region Properties

        [Display(Name = "Project ID")]
        public string Code { get; set; }

        [Display(Name = "Project Name")]
        [Required(ErrorMessage = "This is a required field")]
        public string Name { get; set; }

        [Display(Name = "Project Description")]
        public string Description { get; set; }

        [Display(Name = "PM / ADL / Planner")]
        [Required(ErrorMessage = "This is a required field", AllowEmptyStrings = false)]
        public string PmName { get; set; } //todo: need to add range validation

        [Display(Name = "Application Name")]
        [Required(ErrorMessage = "This is a required field", AllowEmptyStrings = false)]
        public string ApplicationName { get; set; }

        [Display(Name = "Sub Portfolio")]
        [Range(1, Int32.MaxValue, ErrorMessage = "This is a required field")]
        [Required(ErrorMessage = "This is a required field")]
        public int SubPortfolioId { get; set; }

        public string SubPortfolioName { get; set; }
        #endregion

        #region Dropdowns
        public List<SelectListItem> SubPortfolios { get; set; }

        #endregion
    }
}