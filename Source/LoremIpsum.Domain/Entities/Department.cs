using System.Collections.Generic;

namespace LoremIpsum.Domain
{
    /// <summary>
    ///Department table representational model
    /// </summary>
    public class Department
    {
        #region Public Properties

        /// <summary>
        /// The unique Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The name of department
        /// </summary>
        public string Name { get; set; }
  
        /// <summary>
        /// The description of department
        /// </summary>
        public string Description { get; set; }

        #endregion

        #region Navigational Properties

        /// <summary>
        /// The join table between <see cref="Employee"/> and <see cref="Department"/>
        /// </summary>
        public  ICollection<EmployeeDepartment> EmployeeDepartments { get; private set; }


        #endregion
        /// <summary>
        /// Constructor
        /// </summary>
        public Department()
        {
            EmployeeDepartments = new HashSet<EmployeeDepartment>();
        }
    }
}
