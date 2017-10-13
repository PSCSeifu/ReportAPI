using Microsoft.EntityFrameworkCore;
using Report.Data.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Data.ReportItems
{
    public interface IReportItemContext : IDbContextBase
    {
        DbSet<ReportItemEntity> ReportItems { get; set; }
    }

    public class ReportItemContext : DbContextBase, IReportItemContext
    {
        public DbSet<ReportItemEntity> ReportItems { get; set; }
    }
}
