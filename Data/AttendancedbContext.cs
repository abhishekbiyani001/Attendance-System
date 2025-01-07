using Microsoft.EntityFrameworkCore;
using AttendanceSystem.Models;

namespace AttendanceSystem.Data
{
    public class AttendancedbContext : DbContext
    {
        public AttendancedbContext(DbContextOptions<AttendancedbContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
        public DbSet<AttendanceRecord> AttendanceRecords { get; set; }
    }
}