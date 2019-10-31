namespace LoremIpsum.Domain
{
    /// <summary>
    /// Our Settings database table representational model
    /// </summary>
    public class EmployeeMemo 
    {
        #region Public Properties

        /// <summary>
        /// The unique Employee Id
        /// </summary>
        public string EmployeeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string MemoId { get; set; }

        #endregion


        #region Navigational Properties

        /// <summary>
        /// 
        /// </summary>
        public  Employee Employee { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public  Memo Memo { get; set; }


        #endregion
        /// <summary>
        /// 
        /// </summary>
        public EmployeeMemo()
        {
          
        }
    }
}
