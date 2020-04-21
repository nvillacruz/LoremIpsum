using LoremIpsum.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoremIpsum.Web.Server
{
    /// <summary>
    /// Entity Configuration for <see cref="EmployeePosition"/>
    /// </summary>
    public class EmployeePositionConfiguration : IEntityTypeConfiguration<EmployeePosition>
    {

        /// <summary>
        /// Configuration Method
        /// </summary>
        /// <param name="builder"></param>
        public   void Configure(EntityTypeBuilder<EmployeePosition> builder)
        {

           
            builder.HasKey(p => new { p.EmployeeId, p.PositionId });
    

            //One-to-many relationship
            builder.HasOne(p => p.Employee).WithMany(x => x.EmployeePositions).HasForeignKey(y => y.EmployeeId);
            //One-to-many relationship
            builder.HasOne(p => p.Position).WithMany(x => x.EmployeePositions).HasForeignKey(y => y.PositionId);

            builder.ToTable("EmployeePosition");
        }
    }
}
