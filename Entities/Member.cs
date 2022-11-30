using System.ComponentModel.DataAnnotations;

namespace disclone_api.Entities
{
    public class Member
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ServerId { get; set; }
        public string? Nickname { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsActive { get; set; }
        public virtual User? User { get; set; }
        public virtual Server? Server { get; set; }
    }
}
