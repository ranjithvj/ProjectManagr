namespace Models
{
    public class ProjectResource
    {
        public int ProjectResourceId { get; set; }
        public int ProjectId { get; set; }
        public int ResourceId { get; set; }
        public Resource Resource { get; set; }
    }
}
