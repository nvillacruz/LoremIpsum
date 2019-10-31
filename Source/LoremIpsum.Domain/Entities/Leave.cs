using System;

namespace LoremIpsum.Domain
{
    /// <summary>
    /// Our Settings database table representational model
    /// </summary>
    public class Leave : BaseDataModel
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
        public DateTimeOffset LeaveDateFrom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset LeaveDateTo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LeaveTypeId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string Reason { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Comment { get; set; }

        #endregion

        #region Navigational Properties

        /// <summary>
        /// 
        /// </summary>
        public Employee Employee { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public LeaveType LeaveType { get; set; }

        #endregion
        /// <summary>
        /// 
        /// </summary>
        public Leave()
        {


        }
    }
}
