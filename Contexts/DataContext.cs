
using Microsoft.EntityFrameworkCore;

namespace disclone_api
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }
        public DbSet<Student> Students {get; set;}
    }

}

