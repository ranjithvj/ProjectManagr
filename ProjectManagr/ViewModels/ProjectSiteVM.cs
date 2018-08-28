using Models;
using Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;

namespace ProjectManagr.ViewModels
{
    public class ProjectSiteVM : BaseVM
    {
        public ProjectSiteVM()
        {
            //Default
            this.HasBusinessImpact = true;
        }
        public ProjectSiteVM(ProjectSite obj)
        {
            if (obj != null)
            {
                this.Id = obj.Id;
                this.ProjectId = obj.ProjectId;
                this.Apex = obj.Apex;
                this.PotentialValue = obj.PotentialValue;
                this.SiteEngagementStart = obj.SiteEngagementStart;
                this.SiteEngagementEnd = obj.SiteEngagementEnd;
                this.HasBusinessImpact = obj.HasBusinessImpact;
                this.CommentsAndIssues = obj.CommentsAndIssues;
                this.IsResourceRequired = obj.IsResourceRequired;
                this.Attachment = obj.Attachment;
                this.CreatedBy = obj.CreatedBy;
                this.CreatedDate = obj.CreatedDate;
                this.ModifiedBy = obj.ModifiedBy;
                this.ModifiedDate = obj.ModifiedDate;
                this.IsActive = obj.IsActive;

                if (obj.Project != null)
                {
                    this.Code = obj.Project.Code;
                    this.Name = obj.Project.Name;
                    this.Description = obj.Project.Description;
                    this.ApplicationName = obj.Project.ApplicationName;
                    this.SubPortfolioId = obj.Project.SubPortfolio.Id;
                    this.SubPortfolioIdRef = obj.Project.SubPortfolio.Id;
                    this.SubPortfolioName = obj.Project.SubPortfolio.Name;

                    if (obj.Project.Pm != null)
                    {
                        this.PmName = obj.Project.Pm.Name;
                        this.PmId = obj.Project.Pm.Id;
                        this.PmIdRef = obj.Project.Pm.Id;
                    }
                }
                if (obj.EntityStatus != null)
                {
                    this.EntityStatusId = obj.EntityStatus.Id;
                    this.EntityStatusName = obj.EntityStatus.Name;
                    this.ColorCode = obj.EntityStatus.ColorCode;

                }

                if (obj.Site != null)
                {
                    this.SiteId = obj.Site.Id;
                    this.SiteName = obj.Site.Name;
                    if (obj.Site.Country != null)
                    {
                        this.CountryId = obj.Site.Country.Id;
                        this.CountryName = obj.Site.Country.Name;
                    }
                }
                if (obj.SiteItmFeedback != null)
                {
                    this.SiteItmFeedbackId = obj.SiteItmFeedback.Id;
                    this.SiteItmFeedbackName = obj.SiteItmFeedback.Name;
                }
                if (obj.Department != null)
                {
                    this.DepartmentId = obj.Department.Id;
                    this.DepartmentName = obj.Department.Name;
                }
                if (obj.ApplicationType != null)
                {
                    this.ApplicationTypeId = obj.ApplicationType.Id;
                    this.ApplicationTypeName = obj.ApplicationType.Name;
                }
                if (obj.SiteItm != null)
                {
                    this.SiteItmId = obj.SiteItm.Id;
                    this.SiteItmName = obj.SiteItm.Name;
                }

            }
        }

        #region Properties

        [Display(Name = "Project")] //To display a dropdown of names and get the value!
        [Required]
        public int ProjectId { get; set; }

        [Display(Name = "Project ID")]
        public string Code { get; set; }

        [Display(Name = "Project")]
        public string Name { get; set; }

        [Display(Name = "Project Description")]
        public string Description { get; set; }

        [Display(Name = "PM / ADL / Planner")]
        public int PmId { get; set; }

        [Range(1, Int32.MaxValue)]
        public int PmIdRef { get; set; }//To handle the disabled dropdown in the browser

        [Display(Name = "PM / ADL / Planner")]
        public string PmName { get; set; } //todo: need to add range validation

        [Display(Name = "Application")]
        public string ApplicationName { get; set; }

        [Display(Name = "Sub Portfolio")]
        public int SubPortfolioId { get; set; }

        [Range(1, Int32.MaxValue)]
        public int SubPortfolioIdRef { get; set; }//To handle the disabled dropdown in the browser

        [Display(Name = "Sub Portfolio")]
        public string SubPortfolioName { get; set; }

        [Display(Name = "Entity Status")]
        [Range(1, Int32.MaxValue)]
        public int EntityStatusId { get; set; }

        [Display(Name = "Sub Portfolio")]
        public string EntityStatusName { get; set; }

        public string ColorCode { get; set; }

        [Display(Name = "Country")]
        [Range(1, Int32.MaxValue)]
        public int CountryId { get; set; }

        [Display(Name = "Country")]
        public string CountryName { get; set; }

        [Display(Name = "Site")]
        [Range(1, Int32.MaxValue, ErrorMessage = "The Site field is required.")]
        public int SiteId { get; set; }

        [Display(Name = "Site")]
        public string SiteName { get; set; }

        [Display(Name = "Site ITM Feedback")]
        [Range(1, Int32.MaxValue)]
        public int SiteItmFeedbackId { get; set; }

        [Display(Name = "Site ITM Feedback")]
        public string SiteItmFeedbackName { get; set; }

        [Display(Name = "Department")]
        [Range(1, Int32.MaxValue)]
        public int DepartmentId { get; set; }

        [Display(Name = "Site ITM Feedback")]
        public string DepartmentName { get; set; }

        [Display(Name = "Application Type")]
        [Range(1, Int32.MaxValue)]
        public int ApplicationTypeId { get; set; }

        [Display(Name = "Application Type")]
        public string ApplicationTypeName { get; set; }

        [Display(Name = "Site ITM")]
        [Range(1, Int32.MaxValue)]
        public int SiteItmId { get; set; }

        [Display(Name = "Site ITM")]
        public string SiteItmName { get; set; }

        public string Apex { get; set; }

        [Display(Name = "Potential Value")]
        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Currency, ErrorMessage = "Enter a valid currency value")]
        public decimal? PotentialValue { get; set; }

        [Display(Name = "Site Engagement Start Date")]
        [Required(AllowEmptyStrings = false)]
        public DateTime? SiteEngagementStart { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Site Engagement End Date")]
        public DateTime? SiteEngagementEnd { get; set; }

        [Display(Name = "Has Business Impact")]
        public bool HasBusinessImpact { get; set; }

        public string HasBusinessImpactRef //Details page
        {
            get
            {
                if (this.HasBusinessImpact)
                {
                    return "Yes";
                }
                return "No";
                //Changes
            }
        }

        [Display(Name = "Comments and Issues")]
        public string CommentsAndIssues { get; set; }

        [Display(Name = "Is Resource Required")]
        public bool? IsResourceRequired { get; set; }

        public string IsResourceRequiredRef //Details page
        {
            get
            {
                if (this.IsResourceRequired.HasValue && this.IsResourceRequired.Value)
                {
                    return "Yes";
                }
                return "No input";
            }
        }

        public string Attachment { get; set; }
        public bool IsEdit { get; set; }
        public bool IsActive { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateString
        {
            get
            {
                return this.CreatedDate.ToString(Constants.DateFormat.DefaultFormat);
            }
        }
        public string ModifiedDateString
        {
            get
            {
                return this.ModifiedDate.ToString(Constants.DateFormat.DefaultFormat);
            }
        }
        public string SiteEngagementStartString
        {
            get
            {
                return this.SiteEngagementStart?.ToString(Constants.DateFormat.DefaultFormat);
            }
        }
        public string SiteEngagementEndString
        {
            get
            {
                return this.SiteEngagementEnd?.ToString(Constants.DateFormat.DefaultFormat);
            }
        }
        public string CreatedBy { get; set; }

        public bool IsSelected { get; set; }

        //To Show Asterisk near the Projects whose End dates have crossed current date!
        public bool IsProgressBeyondToday { get; set; }

        #endregion

        #region Dropdowns
        public List<SelectListItem> EntityStatuses { get; set; }
        public List<SelectListItem> Projects { get; set; }
        public List<SelectListItem> Managers { get; set; }
        public List<SelectListItem> ApplicationTypes { get; internal set; }
        public List<SelectListItem> Countries { get; internal set; }
        public List<SelectListItem> Departments { get; internal set; }
        public List<SelectListItem> Sites { get; internal set; }
        public List<SelectListItem> SubPortfolios { get; internal set; }
        public List<SelectListItem> SiteItmFeedbacks { get; internal set; }
        public Dictionary<int, List<int>> CountrySiteMap { get; set; }
        #endregion

        #region Validation


        #endregion
    }

    public class ProjectSiteDeleteVM
    {
        public List<int> IdsToBeDeleted { get; set; }
    }
}