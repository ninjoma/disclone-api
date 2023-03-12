using disclone_api.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Server : Entity
{
    public int OwnerId { get; set; }
    public string Name { get; set; }
    public DateTime? CreationDate { get; set; }

    [ForeignKey("ServerId")]
    public virtual ICollection<Invitation>? Invitations { get; set; }
    [ForeignKey("ServerId")]
    public virtual ICollection<Member>? Members { get; set; }
}