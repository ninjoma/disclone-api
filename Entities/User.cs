using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace disclone_api.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Image { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("UserId")]
        public virtual ICollection<Member>? Members { get; set; }

        [ForeignKey("UserId")]
        public virtual ICollection<Invitation>? Invitations { get; set; }

        [ForeignKey("UserId")]
        public virtual ICollection<Message>? Messages { get; set; }

        [ForeignKey("OwnerID")]
        public virtual ICollection<Server>? Servers { get; set; }



    }
}
