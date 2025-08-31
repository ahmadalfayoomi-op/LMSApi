

namespace LMS.Domain.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public string Key { get; set; } = string.Empty;
        public ICollection<Role> Roles { get; set; } = new List<Role>();

    }
}
