using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Project : BaseModel
    {
        [ForeignKey("SubPortfolio")]
        public int SubPortfolioId { get; set; }
        public virtual SubPortfolio SubPortfolio { get; set; }
        
        [MaxLength(50)]
        public string Code { get; set; }

        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }

        [ForeignKey("Pm")]
        public int PmId { get; set; }
        public virtual Manager Pm { get; set; }

        [MaxLength(50)]
        [Required]
        public string ApplicationName { get; set; }
    }

}
