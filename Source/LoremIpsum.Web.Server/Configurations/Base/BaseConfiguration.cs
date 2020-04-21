using LoremIpsum.Core;
using LoremIpsum.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LoremIpsum.Web.Server
{
    /// <summary>
    /// Base configuration for tracked entities
    /// </summary>
    /// <typeparam name="T">A child model from <see cref="BaseDataModel"/></typeparam>
    public class BaseConfiguration<T> : IEntityTypeConfiguration<T>
        where T : BaseDataModel
    {
        /// <summary>
        /// Configuration Method
        /// </summary>
        /// <param name="builder"></param>
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(p => p.CreatedBy).IsRequired().HasMaxLengthAsGuid();
            builder.Property(p => p.ModifiedBy).IsRequired().HasMaxLengthAsGuid();
            builder.Property(p => p.DateCreated).IsRequired().HasDefaultValue(DateTimeOffset.UtcNow);
            builder.Property(p => p.DateModified).IsRequired().HasDefaultValue(DateTimeOffset.UtcNow);
        }


    }
}
