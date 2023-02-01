namespace disclone_api.DTO
{
    public class ServerDTO : BaseDTO
    {
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public virtual List<MemberDTO>? Members { get; set; }
        public virtual List<InvitationDTO>? Invitations { get; set; }
    }
}
