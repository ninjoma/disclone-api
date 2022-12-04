using disclone_api.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Server
{
    [Key]
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public string Name { get; set; }
    public DateTime? CreationDate { get; set; }
    public bool IsActive { get; set; }

    [ForeignKey("ServerId")]
    public virtual ICollection<Invitation>? Invitations { get; set; }
    [ForeignKey("ServerId")]
    public virtual ICollection<Member>? Members { get; set; }



}