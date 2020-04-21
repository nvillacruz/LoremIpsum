using System;
using System.Linq;
using System.Threading.Tasks;
using LoremIpsum.Core;
using static LoremIpsum.Core.GuidHelper;

namespace LoremIpsum.Relational
{
    /// <summary>
    /// Stores and retrieves information about the client application 
    /// such as login credentials, messages, settings and so on
    /// in an SQLite database
    /// </summary>
    public class BaseClientDataStore : IClientDataStore
    {
        #region Protected Members

        /// <summary>
        /// The database context for the client data store
        /// </summary>
        protected ClientDataStoreDbContext mDbContext;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dbContext">The database to use</param>
        public BaseClientDataStore(ClientDataStoreDbContext dbContext)
        {
            // Set local member
            mDbContext = dbContext;
        }

        #endregion

        #region Interface Implementation

        /// <inheritdoc/>
        public async Task<bool> HasCredentialsAsync()
        {
            return await GetLoginCredentialsAsync() != null;
        }

        /// <inheritdoc/>
        public async Task EnsureDataStoreAsync()    
        {
            // Make sure the database exists and is created
            await mDbContext.Database.EnsureCreatedAsync();
        }

        /// <inheritdoc/>
        public Task<LoginCredentialsDataModel> GetLoginCredentialsAsync()
        {
            // Get the first column in the login credentials table, or null if none exist
            return Task.FromResult(mDbContext.LoginCredentials.FirstOrDefault());
        }

        /// <inheritdoc/>
        public async Task SaveLoginCredentialsAsync(LoginCredentialsDataModel loginCredentials)
        {
            // Clear all entries
            mDbContext.LoginCredentials.RemoveRange(mDbContext.LoginCredentials);

            //Create Id
            loginCredentials.Id = GenerateNewGuid();

            // Add new one
            mDbContext.LoginCredentials.Add(loginCredentials);

            // Save changes
            await mDbContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task ClearAllLoginCredentialsAsync()
        {
            // Clear all entries
            mDbContext.LoginCredentials.RemoveRange(mDbContext.LoginCredentials);

            // Save changes
            await mDbContext.SaveChangesAsync();
        }

        #endregion
    }
}
