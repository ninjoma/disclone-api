using disclone_api.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Server
{
    [Key]
    public int ID { get; set; }
    public int OwnerID { get; set; }
    public DateTime? CreationDate { get; set; }
    public bool IsActive { get; set; }

    [ForeignKey("ServerId")]
    public virtual ICollection<Invitation>? Invitations { get; set; }
    [ForeignKey("ServerId")]
    public virtual ICollection<Member>? Members { get; set; }



}