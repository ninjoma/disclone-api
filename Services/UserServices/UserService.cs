using AutoMapper;
using disclone_api.DTO;
using disclone_api.Entities;
using disclone_api.utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace disclone_api.Services
{
    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserService : IUserService
    {

        #region Constructor
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public UserService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        #region Set
        
        public async Task<UserDTO> Add(UserDTO user)
        {
            var newUser = _mapper.Map<User>(user);
            await _context.User.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(newUser);
        }

        public async Task<UserDTO> EditById(UserDTO user)
        {
            var oldUser = await _context.User.FirstOrDefaultAsync(x => x.Id.Equals(user.Id));
            _mapper.Map<UserDTO,User>(user, oldUser);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(oldUser);   
        }

        public async Task<UserDTO> Register(UserDTO user)
        {
            user.IsActive = true;
            user.Password = DCrypt.Encrypt(user.Password);
            var newUser = _mapper.Map<User>(user);
            await _context.User.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(newUser);
        }
        #endregion

        #region Get
        public async Task<UserDetailDTO> GetById(int id, bool isActive = true)
        {
            return _mapper.Map<UserDetailDTO>(await _context.User
                .FirstOrDefaultAsync(x => x.Id.Equals(id) && x.IsActive == isActive));
        }

        public async Task<List<UserDTO>> ListByName(string name, bool isActive = true)
        {
            name ??= "";
            return _mapper.Map<List<UserDTO>>(await _context.User
                .Where(x => x.Username.Contains(name) && x.IsActive == isActive)
                .ToListAsync());
        }
        #endregion

        #region Delete

        public async Task<UserDTO> DeleteById(int id)
        {
            var user = await _context.User.FirstOrDefaultAsync(x => x.Id.Equals(id));
            if (user.IsActive)
            {
                user.IsActive = false;
            } else
            {
                user.IsActive = true;
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(user);
        }
        #endregion



    }
}
