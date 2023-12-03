using Microsoft.EntityFrameworkCore;
using EmployeeManager.Models;

namespace EmployeeManager.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Applicant> Applicants { get; set; }
        public virtual DbSet<EducationQualification> EducationQualifications { get; set; }




    }

}
