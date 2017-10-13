using Microsoft.EntityFrameworkCore;
using Report.Data.Entites;

namespace Report.Data
{
    public interface IDatabase : IDbContextBase
    {
        DbSet<TemplateEntity> Templates { get; set; }
        DbSet<ReportItemEntity> ReportItems { get; set; }
        DbSet<SortItemEntity>   SortItems { get; set; }
        DbSet<FilterItemEntity> FilterItems { get; set; }
    }

    public class Database : DbContextBase,  IDatabase
    {
        public DbSet<TemplateEntity> Templates { get; set; }
        public DbSet<ReportItemEntity> ReportItems { get; set; }
        public DbSet<SortItemEntity> SortItems { get; set; }
        public DbSet<FilterItemEntity> FilterItems { get; set; }
    }
}
