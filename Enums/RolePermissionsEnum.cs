namespace disclone_api.Enums
{
    [Flags]
    public enum RolePermissionsEnum
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
}
