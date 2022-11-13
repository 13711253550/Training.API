using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Domain.Entity;
using Training.Domain.Entity.Drug_Management;

namespace Training.EFCore.Context
{
    /// <summary>
    /// Sql数据库上下文
    /// </summary>
    public class SqlContext : DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
        }
        public DbSet<User> User { get; set; }
        public DbSet<Drug> Drug { get; set; }
        public DbSet<Drug_Type> Drug_Type { get; set; }
        public DbSet<Logistics> Logistics { get; set; }
        public DbSet<Orderdetail> Orderdetail { get; set; }
    }
}
