using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Allocation : BaseModel
    {
        public DateTime EstStart { get; set; }

        public DateTime ActualStart { get; set; }

        public DateTime EstCompletion { get; set; } //Todo: Auto Calculate based on mandays per resource

        public DateTime ActualCompletion { get; set; }

        public string Comments { get; set; }

        public int PlannedLeaveCount { get; set; }

        [ForeignKey("Resource")]
        public int ResourceId { get; set; }

        public virtual Resource Resource { get; set; }

        [ForeignKey("Task")]
        public int TaskId { get; set; }

        public virtual Task Task { get; set; }
    }
}