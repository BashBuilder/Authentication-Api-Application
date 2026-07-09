using AuthenticationApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationApi.Infrastructure.Data
{
    public class AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : DbContext(options)
    {
        public DbSet<AppUser> Users { get; set; }
    }
}
