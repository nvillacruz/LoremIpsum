using System;

namespace LoremIpsum.Domain
{
    /// <summary>
    /// The abstract columns for some entities that require timestamps
    /// However, by inheriting this base class, during migration, it still in the first order after the primary key
    /// Existing issue in Microsoft: Migrations: Order columns of abstract base class properties last in CreateTable
    /// https://github.com/dotnet/efcore/issues/11314
    /// </summary>
    public abstract class BaseDataModel
    {
        #region Public Properties


        /// <summary>
        /// The Date when the entry is created
        /// </summary>
        //[Column(Order =100)]
        public DateTimeOffset DateCreated { get; set; }

        /// <summary>
        /// The date when the entry is last modified
        /// </summary>
        //[Column(Order = 101)]
        public DateTimeOffset DateModified { get; set; }


        /// <summary>
        /// User Id who created the entry
        /// </summary>
        //[Column(Order = 102)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// User Id who last modified the entry
        /// </summary>
        //[Column(Order = 103)]
        public string ModifiedBy  { get; set; }


      
        #endregion
    }
}