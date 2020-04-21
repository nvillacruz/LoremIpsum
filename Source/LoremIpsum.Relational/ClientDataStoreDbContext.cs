using LoremIpsum.Core;
using Microsoft.EntityFrameworkCore;

namespace LoremIpsum.Relational
{
    /// <summary>
    /// The database context for the client data store
    /// </summary>
    public class ClientDataStoreDbContext : DbContext
    {
        #region DbSets 

        /// <summary>
        /// The client login credentials
        /// </summary>
        public DbSet<LoginCredentialsDataModel> LoginCredentials { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ClientDataStoreDbContext(DbContextOptions<ClientDataStoreDbContext> options) : base(options) { }

        #endregion

        #region Model Creating

        /// <summary>
        /// Configures the database structure and relationships
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new LoginCredentialsDataModelConfiguration());
        }

        #endregion
    }
}
