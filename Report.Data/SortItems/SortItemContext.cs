using Microsoft.EntityFrameworkCore;
using Report.Data.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Data.SortItems
{
    public interface ISortItemContext : IDbContextBase
    {
        DbSet<SortItemEntity> SortItems { get; set; }
    }

    public class SortItemContext : DbContextBase, ISortItemContext
    {
        public DbSet<SortItemEntity> SortItems { get; set; }
    }
}
