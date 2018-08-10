using System;
using System.Collections.Generic;
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

        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PmName { get; set; }
        public string ApplicationName { get; set; }
    }

}
