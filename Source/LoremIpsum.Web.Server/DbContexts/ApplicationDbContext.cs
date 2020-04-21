using LoremIpsum.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LoremIpsum.Web.Server
{
    /// <summary>
    /// The database representational model for our application
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        #region Public Properties

        /// <summary>
        /// List of Employees
        /// </summary>
        public DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// List of Departments e.g I.T Department, Finance Department
        /// </summary>
        public DbSet<Department> Departments { get; set; }

        /// <summary>
        /// List of positions available in the company e.g Computer Programmer, General Manager
        /// </summary>
        public DbSet<Position> Positions { get; set; }

        /// <summary>
        /// The join table for <see cref="Employee"/> and <see cref="Department"/>
        /// </summary>
        public DbSet<EmployeeDepartment> EmployeeDepartments { get; set; }
     
        /// <summary>
        /// The join table for <see cref="Employee"/> and <see cref="Position"/>
        /// </summary>
        public DbSet<EmployeePosition> EmployeePositions { get; set; }


        public DbSet<EnterpriseSetting> EnterpriseSettings { get; set; }


        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor, expecting database options passed in
        /// </summary>
        /// <param name="options">The database context options</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        #endregion

        /// <summary>
        /// 
        /// </summary>8
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Import all configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }


    }
}
