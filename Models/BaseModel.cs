using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class BaseModel
    {
       
        [Key, Column(Order = 0)]
        public int Id { get; private set; }
        public bool IsActive { get; set; }
        public DateTime ModifiedDate { get; private set; }
        public string ModifiedBy { get; set; }
    }
}