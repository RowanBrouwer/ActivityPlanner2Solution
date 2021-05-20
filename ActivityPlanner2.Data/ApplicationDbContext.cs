using ActivityPlanner2.Data.ServerModels;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner2.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<Person>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("Server=(localdb)\\mssqllocaldb; Database=ActivityPlanner_InMem; Trusted_Connection=True; MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonOrganizedActivity>()
                .HasKey(pa => new { pa.OrganizerId, pa.OrganizedActivityId });

            modelBuilder.Entity<PersonInvites>()
                .HasKey(pa => new { pa.PersonId, pa.ActivityId, pa.Accepted });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<PersonInvites>  PersonActivities { get; set; }
        public DbSet<PersonOrganizedActivity> PersonOrginizers { get; set; }
    }
}
