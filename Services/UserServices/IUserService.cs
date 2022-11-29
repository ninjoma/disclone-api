using disclone_api.Entities;

namespace disclone_api.Services.UserServices
{
    public interface IUserService
    {
        Task<User> AddEditAsync(User user);

    }
}
