using LoremIpsum.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LoremIpsum.Core;

namespace LoremIpsum.Web.Server
{
    /// <summary>
    /// Entity Configuration for <see cref="EnterpriseSetting"/>
    /// </summary>
    public class EnterpriseSettingConfiguration : IEntityTypeConfiguration<EnterpriseSetting>
    {
        ///<inheritdoc/>
        public void Configure(EntityTypeBuilder<EnterpriseSetting> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).IsRequired().HasMaxLengthAsGuid();

            builder.Property(x => x.CompanyName).HasMaxLength(128);

            builder.ToTable(nameof(EnterpriseSetting));
        }
    }
}
