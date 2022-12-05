using AutoMapper;
using disclone_api.DTOs.UserDTOs;
using disclone_api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace disclone_api.Services.AuthServices
{
    public class AuthService : IAuthService
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public AuthService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDTO> GetUserByClaim(Claim userclaim)
        {
            return _mapper.Map<UserDTO>(await _context.User
                .FirstOrDefaultAsync(u => u.Username == userclaim.Value));
        }
    }

}