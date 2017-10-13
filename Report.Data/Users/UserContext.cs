using Microsoft.EntityFrameworkCore;
using Report.Data.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Data.Users
{
    public interface IUserContext
    {
        DbSet<UserEntity> Users { get; set; }
    }

    public class UserContext
    {
        public DbSet<UserEntity> Users { get; set; }
    }
}
