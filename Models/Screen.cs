using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Shared;

namespace Models
{
    public class Screen : BaseModel
    {
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

        public virtual ICollection<Task> Tasks { get; set; }

        [NotMapped]
        public string SprintsRef
        {
            get
            {
                if (this.Sprints != null)
                {
                    return this.Sprints.ToString();
                }
                return string.Empty;
            }
        }

        [NotMapped]
        public decimal TotalManDays
        {
            get
            {
                return this.EstHours / 7;
            }
        }

        [NotMapped]
        public decimal ManDaysPerResource
        {
            get
            {
                if (this.ResourceCount > 0)
                {
                    return this.TotalManDays / this.ResourceCount;
                }
                return 0;
            }
        }

        [NotMapped]
        public List<int> Sprints { get; set; }
    }
}