using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class EntityStatus
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Country
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Site
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SiteItmFeedback
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Department
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ApplicationType
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SubPortfolio
    {
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}