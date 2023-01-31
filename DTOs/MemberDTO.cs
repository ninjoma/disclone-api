namespace disclone_api.DTO
{
    public class MemberDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ServerId { get; set; }
        public string? Nickname { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsActive { get; set; }
        public virtual UserDTO? User { get; set; }
        public virtual ServerDTO? Server { get; set; }
    }
}
