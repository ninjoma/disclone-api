namespace disclone_api.Entities
{
    public class Member
    {
        #region Propiedades
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ServerId { get; set; }
        public string NickName { get; set; }
        public DateTime JoinDate { get; set; }
        public bool IsActive { get; set; }
        #endregion
    }
}
