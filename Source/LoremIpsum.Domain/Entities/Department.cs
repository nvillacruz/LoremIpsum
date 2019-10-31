using System.Collections.Generic;

namespace LoremIpsum.Domain
{
    /// <summary>
    /// Our Settings database table representational model
    /// </summary>
    public class Department
    {
        #region Public Properties

        /// <summary>
        /// The unique Employee Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
  

        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        #endregion


        #region Navigational Properties

        /// <summary>
        /// 
        /// </summary>
        public  ICollection<EmployeeDepartment> EmployeeDepartments { get; private set; }


        #endregion
        /// <summary>
        /// 
        /// </summary>
        public Department()
        {
            EmployeeDepartments = new HashSet<EmployeeDepartment>();
        }
    }
}
