using LMS.Application.Common;


namespace LMS.Domain.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
    }
}
