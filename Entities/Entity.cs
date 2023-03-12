using System.ComponentModel.DataAnnotations;

namespace disclone_api.Entities
{
    public abstract class Entity
    {
        [Key]
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }
}