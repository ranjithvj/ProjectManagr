using Models;
using Models.Shared;
using System;
using System.Collections.Generic;

namespace ProjectManagr.ViewModels
{
    public class ScreenVM : BaseVM
    {
        public ScreenVM(Screen obj)
        {
            this.Id = obj.Id;
            this.Name = obj.Name;
            this.TechLead = obj.TechLead;
            this.EstCompletion = obj.EstCompletion;
            this.EstRelease = obj.EstRelease;
            this.EstRevisedRelease = obj.EstRevisedRelease;
            this.ActualRelease = obj.ActualRelease;
            this.EstHours = obj.EstHours;
            this.ActualHours = obj.ActualHours;
            this.Comments = obj.Comments;
            this.RevisionReason = obj.RevisionReason;
            this.ResourceCount = obj.ResourceCount;
            this.Status = obj.Status;
            this.SprintsRef = obj.SprintsRef;
            this.TotalManDays = obj.TotalManDays;
            this.ManDaysPerResource = obj.ManDaysPerResource;
            this.Sprints = obj.Sprints;
        }

        #region Properties

        public string Name { get; set; }

        public string TechLead { get; set; }

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

        #endregion Properties
    }
}