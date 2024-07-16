using Microsoft.EntityFrameworkCore;

namespace ContactExam.Models
{
    public class ContactDbContext : DbContext
    {
        public DbSet<Contact> Contact {  get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourseEnrollment> StudentCourseEnrollments { get; set; }

        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Data Source=DESKTOP-I46N7EU\\SQLEXPRESS;Initial Catalog=ContactDB_Janindu;Integrated Security=True;Trust Server Certificate=True";
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
