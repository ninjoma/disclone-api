using disclone_api.Entities;
using Microsoft.EntityFrameworkCore;

namespace disclone_api.Services.UserServices
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creacion o Edicion de Usuarios
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Devuelve el ususario editado o creado</returns>
        public async  Task<User> AddEditAsync(User user)
        {
            if (user.Id != 0){
                return await UpdateUserAsync(user);
            } else
            {
                return await CreateUserAsync(user);
            }
        }

        /// <summary>
        /// Crea un usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Devuelve el usuario creado</returns>
        public async Task<User> CreateUserAsync(User user)
        {
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        /// <summary>
        /// Actualiza un usuario ya existente
        /// </summary>
        /// <param name="user"></param>
        /// <returns>devuelve el objeto del usuario actualizado</returns>
        public async Task<User> UpdateUserAsync(User user)
        {
            var oldUser = await _context.User.FirstOrDefaultAsync(x => x.Id.Equals(user.Id));
            oldUser = user;
            await _context.SaveChangesAsync();
            return user;
        }

    }
}
