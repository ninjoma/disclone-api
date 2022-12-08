using AutoMapper;
using disclone_api.DTOs.UserDTOs;
using Microsoft.EntityFrameworkCore;
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

        public async Task<UserDTO> GetUserByClaim(ClaimsPrincipal clp)
        {
            var userclaim = clp.FindFirst(claim => claim.Type == "sub");
            return _mapper.Map<UserDTO>(await _context.User
                .FirstOrDefaultAsync(u => u.Id == Int32.Parse(userclaim.Value)));
        }
    }

}