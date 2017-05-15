using System.ComponentModel.DataAnnotations.Schema;
using Models.Shared;

namespace Models
{
    public class Task : BaseModel
    {
        public int Name { get; set; }

        public virtual Resource Resource { get; set; }

        [ForeignKey("Resource")]
        public int ResourceId { get; set; }

        public decimal EstHours { get; set; }

        public decimal CompletedHours { get; set; }

        public Constants.TaskType TaskType { get; set; }

        public virtual Screen Screen { get; set; }

        [ForeignKey("Screen")]
        public int ScreenId { get; set; }

        public Constants.Status Status { get; set; }

    }
}