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
        /// <summary>
        /// Employee
        /// </summary>
        public  Employee Employee { get; set; }
        /// <summary>
        /// Position
        /// </summary>
        public  Position Position { get; set; }

        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        public EmployeePosition()
        {
           
        }
    }
}
