using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Domain.Entity;

namespace Training.EFCore.Context
{
    /// <summary>
    /// Sql数据库上下文
    /// </summary>
    public class SqlContext :DbContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
        }
        public DbSet<User> User { get; set; }
    }
}
