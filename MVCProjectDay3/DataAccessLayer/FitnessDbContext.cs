using Microsoft.EntityFrameworkCore;
using MVCProjectDay3.Models;

namespace MVCProjectDay3.DataAccessLayer
{
    public class FitnessDbContext:DbContext
    {
        public FitnessDbContext(DbContextOptions opt):base(opt)
        {
            
        }

        public DbSet<Trainer>Trainers { get; set; }
        public DbSet<Course>Courses { get; set; }
    }
}
