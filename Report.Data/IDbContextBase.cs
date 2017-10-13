using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Report.Data
{
    public interface IDbContextBase
    {
        //Task<int> SaveChangesAsync();
        int SaveChanges();
        void SetModified(object entity);
        DatabaseFacade Database { get; }
    }

    public class DbContextBase : DbContext, IDbContextBase
    {
        IConfiguration Configuration { get; set; }

        public DbContextBase()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json");
               

            Configuration = builder.Build();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var conn = Configuration["Data:DefaultConnection:ConnectionString"];
            optionsBuilder.UseSqlServer(conn);
        }

        public void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void DisableAutoDetectChanges()
        {
            this.ChangeTracker.AutoDetectChangesEnabled = false;
        }

        //public async Task<int> SaveChangesAsync()
        //{
        //    return await this.SaveChangesAsync();
        //}
    }
}
