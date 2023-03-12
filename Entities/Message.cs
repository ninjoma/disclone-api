using System.ComponentModel.DataAnnotations;

namespace disclone_api.Entities
{
    public class Message : Entity
    {
        public int UserId { get; set; }
        public int ChannelId { get; set; }
        public string? Content { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual User? User { get; set; }
        public virtual Channel? Channel { get; set; }

    }
}
