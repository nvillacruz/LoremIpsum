using System;
using System.Collections.Generic;

namespace LoremIpsum.Domain
{
    /// <summary>
    /// Our Settings database table representational model
    /// </summary>
    public class Employee
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
        public string Gender { get; set; }
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
        /// 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PhoneNumber { get; set; }
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
        public string Scheme { get; set; }


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
        public virtual ICollection<SalaryHistory> SalaryHistories { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<EmployeeDepartment> EmployeeDepartments { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<Timekeeping> Timekeepings { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<EmployeeMemo> EmployeeMemos { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<Leave> Leaves { get; private set; }

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
            SalaryHistories = new HashSet<SalaryHistory>();
            EmployeeDepartments = new HashSet<EmployeeDepartment>();
            Timekeepings = new HashSet<Timekeeping>();
            Leaves = new HashSet<Leave>();
            EmployeePositions = new HashSet<EmployeePosition>();
        }
    }
}
