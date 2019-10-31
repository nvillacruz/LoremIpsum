using System;

namespace LoremIpsum.Domain
{
    /// <summary>
    /// Our Settings database table representational model
    /// </summary>
    public class SalaryHistory
    {
        #region Public Properties

        /// <summary>
        /// The unique Employee Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string EmployeeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset EffectivityDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal BasicSalary { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal Deminimis { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal Allowance { get; set; }




        #endregion

        #region Navigational Properties

        /// <summary>
        /// 
        /// </summary>
        public  Employee Employee { get; set; }


        #endregion
        /// <summary>
        /// 
        /// </summary>
        public SalaryHistory()
        {
           
         
        }
    }
}
