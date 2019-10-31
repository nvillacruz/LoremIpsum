using System.Collections.Generic;

namespace LoremIpsum.Domain
{
    /// <summary>
    /// Our Settings database table representational model
    /// </summary>
    public class Memo : BaseDataModel
    {
        #region Public Properties

        /// <summary>
        /// The unique Employee Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SendToId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        #endregion


        #region Navigational Properties
        /// <summary>
        /// 
        /// </summary>
        public  ICollection<EmployeeMemo> EmployeeMemos { get;  private set; }


        #endregion
        /// <summary>
        /// 
        /// </summary>
        public Memo()
        {
            EmployeeMemos = new HashSet<EmployeeMemo>();
        }
    }
}
