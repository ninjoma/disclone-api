using disclone_api.Services.UserServices;
namespace disclone_api.Services
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection collection)
        {
            collection.AddTransient<IUserService, UserService>();
        }
    }
}
