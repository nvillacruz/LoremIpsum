using System;
using System.Collections.Generic;

namespace LoremIpsum.Domain
{
    /// <summary>
    /// Employee database table representational model
    /// </summary>
    public class Employee : BaseDataModel
    {
        #region Public Properties

        /// <summary>
        /// The unique Employee Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>

        public string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Gender Gender { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset? BirthDate { get; set; }
        /// <summary>
        /// The users first name
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// The users last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// User's middle name
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string SSSNnumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TINNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PHICNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string HDMFNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset HireDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTimeOffset? ResignedDate { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string Address { get; set; }
        #endregion

        #region Navigational Properties

        /// <summary>
        /// 
        /// </summary>
        public virtual ApplicationUser User { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<EmployeeDepartment> EmployeeDepartments { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<EmployeePosition> EmployeePositions { get; set; }

        #endregion
        /// <summary>
        /// 
        /// </summary>
        public Employee()
        {
            EmployeeDepartments = new HashSet<EmployeeDepartment>();
            EmployeePositions = new HashSet<EmployeePosition>();
        }
    }

    /// <summary>
    /// Gender enumeration
    /// </summary>
    public enum Gender
    {
        /// <summary>
        /// Male
        /// </summary>
        Male,
        /// <summary>
        /// Female
        /// </summary>
        Female
    }

}
