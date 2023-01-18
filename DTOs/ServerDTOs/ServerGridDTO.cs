namespace disclone_api.DTO
{
    public class ServerGridDTO
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsActive { get; set; }
        public virtual List<MemberDTO>? Members { get; set; }
        public virtual List<InvitationDTO>? Invitations { get; set; }
    }
}
