using LoremIpsum.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LoremIpsum.Core;

namespace LoremIpsum.Web.Server
{
    /// <summary>
    /// Entity Configuration for <see cref="Employee"/>
    /// </summary>
    public class EmployeeConfiguration : BaseConfiguration<Employee>
    {

        /// <summary>
        /// Configuration Method
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<Employee> builder)
        {
            base.Configure(builder);

            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).IsRequired().HasMaxLengthAsGuid();
            builder.Property(x => x.Code).HasMaxLength(8);
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(64);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(64);
            builder.Property(p => p.MiddleName).IsRequired().HasMaxLength(64);
            builder.Property(p => p.SSSNnumber).IsRequired().HasMaxLength(20);
            builder.Property(p => p.PHICNumber).IsRequired().HasMaxLength(20);
            builder.Property(p => p.HDMFNumber).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Gender);

            //One-to-One relationship
            builder.HasOne(p => p.User).WithOne(x => x.Employee).HasForeignKey<Employee>(y => y.UserId);

            builder.ToTable("Employee");
        }
    }
}
