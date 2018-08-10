using Models;
using Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagr.ViewModels
{
    public class ScreenVM : BaseVM
    {
        public ScreenVM()
        {
        }

        #region Properties
        [Required]
        public string Name { get; set; }

        public string TechLead { get; set; }

        [Required(ErrorMessage = "This is a required field")]
        [Display(Name = "Estimated Completion")]
        public DateTime EstCompletion { get; set; }

        public DateTime EstRelease { get; set; }

        public DateTime EstRevisedRelease { get; set; }

        public DateTime ActualRelease { get; set; }

        public decimal EstHours { get; set; }

        public decimal ActualHours { get; set; }

        public string Comments { get; set; }

        public string RevisionReason { get; set; }

        public int ResourceCount { get; set; }

        public Constants.Status Status { get; set; }

        public string SprintsRef { get; set; }

        public decimal TotalManDays { get; set; }

        public decimal ManDaysPerResource { get; set; }

        public List<int> Sprints { get; set; }
        public bool IsEdit { get; set; }

        #endregion Properties
    }
}