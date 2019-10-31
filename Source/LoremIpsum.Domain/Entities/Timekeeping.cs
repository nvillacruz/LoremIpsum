using System;

namespace LoremIpsum.Domain
{
    /// <summary>
    /// Our Settings database table representational model
    /// </summary>
    public class Timekeeping
    {
        #region Public Properties

        /// <summary>
        /// The unique Employee Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset? TimeIn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset? TimeOut { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string EmployeeId { get; set; }

        #endregion

        #region Navigational Properties

        /// <summary>
        /// 
        /// </summary>
        public Employee Employee { get; set; }


        #endregion
        /// <summary>
        /// 
        /// </summary>
        public Timekeeping()
        {


        }
    }
}
