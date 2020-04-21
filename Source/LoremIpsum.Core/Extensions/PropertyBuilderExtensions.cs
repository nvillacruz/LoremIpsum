using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LoremIpsum.Core
{
    /// <summary>
    /// 
    /// </summary>
    public  static class PropertyBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="precision"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static PropertyBuilder<decimal?> HasPrecision(this PropertyBuilder<decimal?> builder, int precision, int scale)
        {
            return builder.HasColumnType($"decimal({precision},{scale})");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="precision"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static PropertyBuilder<decimal> HasPrecision(this PropertyBuilder<decimal> builder, int precision, int scale)
        {
            return builder.HasColumnType($"decimal({precision},{scale})");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static PropertyBuilder<decimal> HasPrecisionTwo(this PropertyBuilder<decimal> builder)
        {
            return builder.HasColumnType($"decimal({18},{2})");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static PropertyBuilder<string> HasMaxLengthAsName(this PropertyBuilder<string> builder)
        {
            return builder.HasMaxLength(256);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static PropertyBuilder<string> HasMaxLengthAsDefaultEntity(this PropertyBuilder<string> builder)
        {
            return builder.HasMaxLength(64);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static PropertyBuilder<string> HasMaxLengthAsGuid(this PropertyBuilder<string> builder)
        {
            return builder.HasMaxLength(36);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static PropertyBuilder<string> AsVarchar(this PropertyBuilder<string> builder)
        {
            return builder.HasColumnType("varchar");
        }
    }
}
