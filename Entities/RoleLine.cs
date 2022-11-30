using System.ComponentModel.DataAnnotations;

namespace disclone_api.Entities
{
    public class RoleLine
    {
        [Key]
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public virtual Member? Member { get; set; }
        public virtual Role? Role { get; set; }

    }
}
