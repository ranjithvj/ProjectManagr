
namespace Models
{
    public class Project : BaseModel
    {
        public int ProjectId { get; set; }

        public string Name { get; set; }

        public string NickName { get; set; }
    }
}