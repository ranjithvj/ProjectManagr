using System;
using System.ComponentModel.DataAnnotations;
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

        [MaxLength(50)]
        public string Apex { get; set; }
        
        [Required]
        public decimal? PotentialValue { get; set; }

        public string SiteItm { get; set; }

        [Required]
        public DateTime? SiteEngagementStart { get; set; }

        [Required]
        public DateTime? SiteEngagementEnd { get; set; }

        public bool HasBusinessImpact { get; set; } 

        [MaxLength(100)]
        public string CommentsAndIssues { get; set; }

        public bool? IsResourceRequired { get; set; }

        [MaxLength(100)]
        public string Attachment { get; set; }
    }
}