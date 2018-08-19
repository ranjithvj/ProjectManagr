using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class EntityStatus
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string ColorCode { get; set; }
    }

    public class Country
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public virtual ICollection<Site> Site { get; set; }
    }

    public class Site
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [ForeignKey("Country")]
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }

    }

    public class SiteItmFeedback
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }

    public class Department
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }

    public class ApplicationType
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }

    public class SubPortfolio
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}