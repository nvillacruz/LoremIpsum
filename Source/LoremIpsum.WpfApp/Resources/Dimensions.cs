using System.Windows;

namespace LoremIpsum.WpfApp
{
    /// <summary>
    /// Default dimension for all controls
    /// </summary>
    public class Dimensions
    {
        /// <summary>
        /// Default ResourceKey for ConrerRadius
        /// </summary>
        public static ComponentResourceKey CornerRadius => new ComponentResourceKey(typeof(Dimensions), "CornerRadius");

        /// <summary>
        /// Default ResourceKey for BorderThickness
        /// </summary>
        public static ComponentResourceKey BorderThickness => new ComponentResourceKey(typeof(Dimensions), "BorderThickness");
        /// <summary>
        /// Default ResourceKey for HorizontalSpace
        /// </summary>
        public static ComponentResourceKey HorizontalSpace => new ComponentResourceKey(typeof(Dimensions), "HorizontalSpace");
        /// <summary>
        /// Default ResourceKey for VerticalSpace
        /// </summary>
        public static ComponentResourceKey VerticalSpace => new ComponentResourceKey(typeof(Dimensions), "VerticalSpace");
    }
}
