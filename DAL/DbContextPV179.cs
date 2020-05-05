using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using DAL.Config;
using DAL.Entities;
using DAL.Initializers;

namespace DAL
{
    public class DbContextPV179 : DbContext
    {
        /// <summary>
        /// Non-parametric ctor used by data access layer
        /// </summary>
        public DbContextPV179() : base(EntityFrameworkInstaller.ConnectionString)
        {
            // force load of EntityFramework.SqlServer.dll into build
            var instance = SqlProviderServices.Instance;
            Database.SetInitializer(new SystemDbInitializer());
        }

        /// <summary>
        /// Ctor with db connection, required by data access layer tests
        /// </summary>
        /// <param name="connection">The database connection</param>
        public DbContextPV179(DbConnection connection) : base(connection, true)
        {
            Database.CreateIfNotExists();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<JobOffer> JobOffers { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<FavoriteJob> FavoriteJobs { get; set; }
        public DbSet<KeywordsJobOffer> KeywordsJobOffers { get; set; }
        public DbSet<KeywordsApplicant> KeywordsApplicants { get; set; }        
    }
}
