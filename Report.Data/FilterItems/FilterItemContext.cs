using Microsoft.EntityFrameworkCore;
using Report.Data.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Data.FilterItems
{
    public interface IFilterItemContext : IDbContextBase
    {
        DbSet<FilterItemEntity> FilterItems { get; set; }
    }

    public class FilterItemContext : DbContextBase, IFilterItemContext
    {
        public DbSet<FilterItemEntity> FilterItems { get; set; }
    }
}
