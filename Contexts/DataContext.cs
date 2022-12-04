using Microsoft.EntityFrameworkCore;
using disclone_api.Entities;

namespace disclone_api
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
        }
        public DbSet<Channel> Channel { get; set;}
        public DbSet<Invitation> Invitation {get; set;}
        public DbSet<Member> Member {get; set;}
        public DbSet<Message> Message { get; set;}
        public DbSet<Role> Role { get; set; }
        public DbSet<RoleLine> RoleLines { get; set;}
        public DbSet<Server> Server { get; set;}
        public DbSet<User> User {get; set;}
    }

}

