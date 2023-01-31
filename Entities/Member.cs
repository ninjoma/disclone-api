using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace disclone_api.Entities
{
    public class Member : Entity
    {
        public int UserId { get; set; }
        public int ServerId { get; set; }
        public string? Nickname { get; set; }
        public DateTime JoinDate { get; set; }
        public virtual User? User { get; set; }
        public virtual Server? Server { get; set; }

        [ForeignKey("MemberId")]
        public ICollection<RoleLine>? RoleLines { get; set; }
    }
}
