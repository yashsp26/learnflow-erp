using LearnFlowERP.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace LearnFlowERP.Infrastructure.Persistence.AppDbContext
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new AppDbContext(
                optionsBuilder.Options,
                new DummyCurrentUserService(),
                new HttpContextAccessor()   // ✅ FIX HERE
            );
        }
    }
    public class DummyCurrentUserService : ICurrentUserService
    {
        public long? UserId => null;
        public long? TenantId => null;
    }
}