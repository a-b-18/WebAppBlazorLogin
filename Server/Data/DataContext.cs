using Microsoft.EntityFrameworkCore;
using WebAppBlazorLogin.Server.Entities;

namespace WebAppBlazorLogin.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<UserDetail> UserDetails { get; set; }
    }
}
