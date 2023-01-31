using System.ComponentModel.DataAnnotations;

namespace disclone_api.Entities
{
    public class RoleLine : Entity
    {
        public int MemberId { get; set; }
        public int RoleId { get; set; }
        public virtual Member? Member { get; set; }
        public virtual Role? Role { get; set; }

    }
}
