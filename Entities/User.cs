using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace disclone_api.Entities
{
    [Index(nameof(Username), IsUnique = true)]
    [Index(nameof(Email), IsUnique = true)]
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        public string? Image { get; set; }
        public DateTime CreationDate { get; set; }
        [Required]
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
