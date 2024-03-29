using AutoMapper;
using disclone_api.DTO;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace disclone_api.Services
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
            var userclaim = Int32.Parse(clp.FindFirstValue(JwtRegisteredClaimNames.Sub));
            return _mapper.Map<UserDTO>(await _context.User
                .FirstOrDefaultAsync(u => u.Id == userclaim));
        }
    }

}