using Assigment_2.Model;
using Microsoft.EntityFrameworkCore;

namespace Assigment_2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base(options){

        }
        public DbSet<Student> Students { get; set; }
    }
}