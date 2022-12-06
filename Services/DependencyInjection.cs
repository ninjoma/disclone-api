﻿using AutoMapper;
using disclone_api.DTOs;
using disclone_api.Services.InvitationServices;
using disclone_api.Services.MemberServices;
using disclone_api.Services.ServerServices;
using disclone_api.Services.UserServices;
using disclone_api.Services.AuthServices;
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
            collection.AddTransient<IAuthService, AuthService>();
        }
    }
}
