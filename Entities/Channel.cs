using disclone_api.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace disclone_api.Entities
{
    public class Channel
    {
        [Key]
        public int Id { get; set; }
        public int ServerId { get; set; }
        public string Name { get; set; }
        public ChannelTypeEnum Type { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
        public virtual Server? Server { get; set; }
        [ForeignKey("ChannelId")]
        public virtual ICollection<Message>? Messages { get; set; }
    }
}
