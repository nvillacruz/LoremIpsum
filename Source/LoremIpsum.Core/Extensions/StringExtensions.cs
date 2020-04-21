using System;

namespace LoremIpsum.Core
{
    /// <summary>
    /// 
    /// </summary>
    public  static class StringExtensions
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string NormalizedUpper(this string str)
        {
            return str.ToUpper().Trim();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string NormalizedLower(this string str)
        {
            return str.ToLower().Trim();
        }
    }

    public static class GuidHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GenerateNewGuid()
        {
            return Guid.NewGuid().ToString();
        }

    }
}
