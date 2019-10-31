using System;

namespace LoremIpsum.Domain
{
    /// <summary>
    /// Our Settings database table representational model
    /// </summary>
    public class EmployeePosition
    {
        #region Public Properties

        /// <summary>
        /// The unique Employee Id
        /// </summary>
        public string EmployeeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PositionId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset EffectivityDate { get; set; }



        #endregion


        #region Navigational Properties

        public  Employee Employee { get; set; }
        public  Position Position { get; set; }

        #endregion
        /// <summary>
        /// 
        /// </summary>
        public EmployeePosition()
        {
           
        }
    }
}
