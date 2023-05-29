using Microsoft.EntityFrameworkCore;
using WebApiDemoG.Entities;

namespace WebApiDemoG.Data
{
    public class StudentDBContext:DbContext
    {
        public StudentDBContext(DbContextOptions<StudentDBContext> options)
            : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
    }
}
