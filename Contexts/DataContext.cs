using Microsoft.EntityFrameworkCore;
using disclone_api.Entities;

namespace disclone_api
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {
        }
        public DbSet<Student> Students {get; set;}
        public DbSet<User> User {get; set;}
        public DbSet<Member> Member {get; set;}
        public DbSet<Invitation> Invitation {get; set;}
    }

}

