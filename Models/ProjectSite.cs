using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class ProjectSite : BaseModel
    {
        [ForeignKey("Project")]
        public int ProjectId { get; set; }

        public virtual Project Project { get; set; }

        [ForeignKey("EntityStatus")]
        public int EntityStatusId { get; set; }

        public virtual EntityStatus EntityStatus { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }

        public virtual Country Country { get; set; }

        [ForeignKey("Site")]
        public int SiteId { get; set; }

        public virtual Site Site { get; set; }

        [ForeignKey("SiteItmFeedback")]
        public int SiteItmFeedbackId { get; set; }

        public virtual SiteItmFeedback SiteItmFeedback { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        [ForeignKey("ApplicationType")]
        public int ApplicationTypeId { get; set; }

        public virtual ApplicationType ApplicationType { get; set; }

        public string Apex { get; set; }
        public string PotentialValue { get; set; }
        public string SiteItm { get; set; }
        public DateTime? SiteEngagementStart { get; set; }
        public DateTime? SiteEngagementEnd { get; set; }
        public string HasBusinessImpact { get; set; } //Y or N : Yes or No -- ~Handle in C#
        public string CommentsAndIssues { get; set; }
        public string IsResourceRequired { get; set; } //Y or N : Yes or No  -- ~Handle in c#
        public string Attachment { get; set; }


    }
}