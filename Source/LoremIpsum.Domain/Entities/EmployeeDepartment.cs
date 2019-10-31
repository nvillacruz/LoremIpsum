
using System;

namespace LoremIpsum.Domain
{
    /// <summary>
    /// Our Settings database table representational model
    /// </summary>
    public class EmployeeDepartment
    {
        #region Public Properties

        /// <summary>
        /// The unique Employee Id
        /// </summary>
        public string EmployeeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DepartmentID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset EffectivityDate { get; set; }

        #endregion


        #region Navigational Properties

        /// <summary>
        /// 
        /// </summary>
        public   Employee Employee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public  Department Department { get; set; }


        #endregion
        /// <summary>
        /// 
        /// </summary>
        public EmployeeDepartment()
        {
           
        }
    }
}
