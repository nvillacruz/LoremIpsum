using LoremIpsum.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoremIpsum.Web.Server
{
    /// <summary>
    /// Entity Configuration for <see cref="EmployeeDepartment"/>
    /// </summary>
    public class EDepartmentConfiguration : IEntityTypeConfiguration<EmployeeDepartment>
    {

        /// <summary>
        /// Configuration Method
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<EmployeeDepartment> builder)
        {
            builder.HasKey(p => new { p.EmployeeId, p.DepartmentID });

            builder.HasOne(p => p.Department).WithMany(x => x.EmployeeDepartments).HasForeignKey(y => y.DepartmentID);
            builder.HasOne(p => p.Employee).WithMany(x => x.EmployeeDepartments).HasForeignKey(y => y.EmployeeId);

            builder.ToTable("EmployeeDepartment");
        }
    }
}
