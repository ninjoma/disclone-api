using AutoMapper;
using disclone_api.DTO;

namespace disclone_api.Services
{
    public static class DependencyInjection
    {

        public static void RegisterServices(this IServiceCollection collection)
        {
            collection.AddTransient<IUserService, UserService>();
            collection.AddTransient<IServerService, ServerService>();
            collection.AddTransient<IMemberService, MemberService>();
            collection.AddTransient<IInvitationService, InvitationService>();
            collection.AddTransient<IChannelService, ChannelService>();
            collection.AddTransient<IAuthService, AuthService>();
            collection.AddTransient<IMessageService, MessageService>();
            collection.AddTransient<ILoggerService, LoggerService>();
        }
    }
}
