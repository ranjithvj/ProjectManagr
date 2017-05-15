using System.ComponentModel.DataAnnotations.Schema;
using Models.Shared;

namespace Models
{
    public class Resource : BaseModel
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public Constants.Role Role { get; set; }
    }
}