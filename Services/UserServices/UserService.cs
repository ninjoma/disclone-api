using AutoMapper;
using disclone_api.DTOs.UserDTOs;
using disclone_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace disclone_api.Services.UserServices
{
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
        /// <summary>
        /// Creacion o Edicion de Usuarios
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Devuelve el ususario editado o creado</returns>
        public async Task<UserDTO> AddEditAsync(UserDTO user)
        {
            if (user.Id != 0)
            {
                return await UpdateUserAsync(user);
            }
            else
            {
                return await CreateUserAsync(user);
            }
        }

        /// <summary>
        /// Crea un usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Devuelve el usuario creado</returns>
        public async Task<UserDTO> CreateUserAsync(UserDTO user)
        {
            await _context.User.AddAsync(_mapper.Map<User>(user));
            await _context.SaveChangesAsync();
            return user;
        }

        /// <summary>
        /// Actualiza un usuario ya existente
        /// </summary>
        /// <param name="user"></param>
        /// <returns>devuelve el objeto del usuario actualizado</returns>
        public async Task<UserDTO> UpdateUserAsync(UserDTO user)
        {
            var oldUser = await _context.User.FirstOrDefaultAsync(x => x.Id.Equals(user.Id));
            oldUser = _mapper.Map<User>(user);
            await _context.SaveChangesAsync();
            return user;
        }
        #endregion

        #region Get
        public async Task<UserDTO> GetById(int id)
        {
            return _mapper.Map<UserDTO>(await _context.User
                .FirstOrDefaultAsync(x => x.Id.Equals(id) && x.IsActive != false));
        }

        public async Task<List<UserDTO>> ListByName(string name)
        {
            return _mapper.Map<List<UserDTO>>(await _context.User
                .Where(x => x.Username.Contains(name) && x.IsActive != false)
                .ToListAsync());
        }
        #endregion

        #region Delete
        public async Task<UserDTO> ToggleInactiveById(int id)
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
