using Microsoft.EntityFrameworkCore;
using Report.Data.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Data.Templates
{
    public interface ITemplateContext : IDbContextBase
    {
        DbSet<TemplateEntity> Templates { get; set; }     
    }

    public class TemplateContext : DbContextBase, ITemplateContext
    {
        public DbSet<TemplateEntity> Templates { get; set; }     
    }
}
