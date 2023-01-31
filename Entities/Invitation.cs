using System.ComponentModel.DataAnnotations;

namespace disclone_api.Entities
{
    public class Invitation : Entity
    {
        public int ServerId { get; set; }
        public int Receiver { get; set; }
        public string? Url { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public virtual User? User { get; set; }
        public virtual Server? Server { get; set; }
    }
}
