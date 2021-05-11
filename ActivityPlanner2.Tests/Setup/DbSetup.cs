using ActivityPlanner2.Data;
using ActivityPlanner2.Shared;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityPlanner2.Tests
{
    class DbSetup
    {
        public static ValueTuple<DbContextOptions, IOptions<OperationalStoreOptions>> CreateNewContextOptions()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid()
                .ToString())
                .EnableSensitiveDataLogging().Options;

            var optionsStore = Options.Create(new OperationalStoreOptions());

            var returnObject = (options, optionsStore);

            return returnObject;
        }

        public static UserManager<Person> CreateUsermanager(ApplicationDbContext context)
        {
            IUserStore<Person> userStore = new UserStore<Person>(context);

            IPasswordHasher<Person> passwordHasher = new PasswordHasher<Person>();

            var manager = new UserManager<Person>(userStore, null, passwordHasher, null, null, null, null, null, null);

            return manager;
        }

        public static ApplicationDbContext CreateContext((DbContextOptions, IOptions<OperationalStoreOptions>) options)
        {
            return new ApplicationDbContext(options.Item1, options.Item2);
        }

        public static void Seed(UserManager<Person> manager, ApplicationDbContext context)
        {
            ApplicationDbSeed.Seed(manager, context, true);
        }
    }
}
