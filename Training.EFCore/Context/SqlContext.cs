using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Domain.Entity.Drug_Management;
using Training.Domain.Entity.Seckill;
using Training.Domain.Entity.UserEntity;
using Training.Domain.Entity.UserEntity.User;

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
        public DbSet<Doctor> Doctor { get; set; }
        public DbSet<Label> Label { get; set; }
        public DbSet<Clinical_Reception> Clinical_Reception { get; set; }
        public DbSet<Doctor_label> Doctor_label { get; set; }
        public DbSet<Inquiry_Prescription> Inquiry_Prescription { get; set; }
        public DbSet<Inquiry_Result> Inquiry_Result { get; set; }
        public DbSet<DrugOrder> DrugOrder { get; set; }
        public DbSet<goods> goods { get; set; }
        public DbSet<seckill_Activity> seckill_Activity { get; set; }
        public DbSet<Seckill_Goods> Seckill_Goods { get; set; }
        public DbSet<SeckillOrder> SeckillOrder { get; set; }
    }
}
