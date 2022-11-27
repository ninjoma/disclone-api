[Flags]
public enum Permissions
{
    None = 0,
    CanEraseMessages = 1,
    CanWrite = 2,
    EnterChannel = 4,
    LeaveChannel = 8,
    CanMute = 16,
    CanMuteOthers = 32,
    CanKick = 64,
    CanBan = 128,
    CanDeafOthers = 256,
    CanModifyRole = 512,
    CanModifyChannel = 1024,
    CanModifyServer = 2048,
    CanChangeNickname = 4096,
    CanChangeOtherNicknames = 8192,
    CanMoveOthers = 16384,
    CanCreateInvitation = 32768,
}

public class Role
{
    public int Id { get; set; }
    //public Server ParentServer {get; set; }
    public string? Name { get; set; }
    public string? Color { get; set; }
    public Permissions Permits { get; set; }
    public DateTime CreationDate {get; set; }
    public bool isActive {get; set; }

}