using disclone_api.Entities;
using disclone_api.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Role
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Color { get; set; }
    public RolePermissionsEnum Permits { get; set; }
    public DateTime CreationDate {get; set; }
    public bool isActive {get; set; }
    public virtual Server? Server { get; set; }

    [ForeignKey("RoleId")]
    public virtual ICollection<RoleLine>? RoleLines { get; set;}
}