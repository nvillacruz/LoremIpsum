using LoremIpsum.Core;
using LoremIpsum.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoremIpsum.Relational
{
    /// <summary>
    /// Entity Configuration for <see cref="LoginCredentialsDataModel"/>
    /// </summary>
    public class LoginCredentialsDataModelConfiguration : IEntityTypeConfiguration<LoginCredentialsDataModel>
    {

        /// <summary>
        /// Configuration Method
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<LoginCredentialsDataModel> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasMaxLengthAsGuid();
        }
    }
}
