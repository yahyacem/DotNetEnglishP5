using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DotNetEnglishP5.Data;

namespace DotNetEnglishP5.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Car> Car { get; set; }
        public virtual DbSet<Make> Make { get; set; }
        public virtual DbSet<Model> Model { get; set; }
        public virtual DbSet<Image> Image { get; set; }
    }
}