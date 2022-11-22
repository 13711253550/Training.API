﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Training.EFCore.Context;

#nullable disable

namespace Training.EFCore.Migrations
{
    [DbContext(typeof(SqlContext))]
    partial class SqlContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Training.Domain.Entity.CS", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Pid")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("CS");
                });

            modelBuilder.Entity("Training.Domain.Entity.Drug_Management.Drug", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Drug_Code")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Drug_Image")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Drug_IsPrescription")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("Drug_IsShelves")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Drug_Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Drug_Price")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Drug_Specification")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Drug_Stock")
                        .HasColumnType("int");

                    b.Property<int>("Drug_Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Drug");
                });

            modelBuilder.Entity("Training.Domain.Entity.Drug_Management.Drug_Type", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Drug_Type_Image")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Drug_Type_IsShelves")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Drug_Type_Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("Drug_Type_Stock")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Drug_Type");
                });

            modelBuilder.Entity("Training.Domain.Entity.Drug_Management.Logistics", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("logistics_company_name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Logistics");
                });

            modelBuilder.Entity("Training.Domain.Entity.Drug_Management.Orderdetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("DrugId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<decimal>("delivery_fee")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("drug_number")
                        .HasColumnType("int");

                    b.Property<int>("logistics_company")
                        .HasColumnType("int");

                    b.Property<string>("logistics_number")
                        .HasColumnType("longtext");

                    b.Property<int>("order_status")
                        .HasColumnType("int");

                    b.Property<DateTime>("order_time")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("payment_code")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("payment_number")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("remarks")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("DrugId");

                    b.HasIndex("UserId");

                    b.ToTable("Orderdetail");
                });

            modelBuilder.Entity("Training.Domain.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("UserSex")
                        .HasColumnType("int");

                    b.Property<string>("account")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("address")
                        .HasColumnType("longtext");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("phone")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Training.Domain.Entity.Drug_Management.Orderdetail", b =>
                {
                    b.HasOne("Training.Domain.Entity.Drug_Management.Drug", "Drug")
                        .WithMany()
                        .HasForeignKey("DrugId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Training.Domain.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Drug");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
