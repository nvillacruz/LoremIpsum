﻿namespace LoremIpsum.Core
{
    /// <summary>
    /// The result of a login request via API
    /// </summary>
    public class VerifyEmailResultApiModel
    {
        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Message { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public VerifyEmailResultApiModel()
        {

        }

        #endregion
    }
}
