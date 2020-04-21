using LoremIpsum.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LoremIpsum.Core;

namespace LoremIpsum.Web.Server
{
    /// <summary>
    /// Entity Configuration for <see cref="Position"/>
    /// </summary>
    public class PositionConfiguration : IEntityTypeConfiguration<Position>
    {
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).IsRequired().HasMaxLengthAsGuid();

            builder.Property(x => x.Name).HasMaxLength(64);
            builder.Property(x => x.Description).HasMaxLength(256);

            builder.ToTable("Position");
        }
    }
}
