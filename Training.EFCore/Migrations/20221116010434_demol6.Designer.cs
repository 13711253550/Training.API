﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Training.EFCore.Context;

#nullable disable

namespace Training.EFCore.Migrations
{
    [DbContext(typeof(SqlContext))]
    [Migration("20221116010434_demol6")]
    partial class demol6
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

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

                    b.Property<int>("User_Id")
                        .HasColumnType("int");

                    b.Property<decimal>("actual_amount")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("consignee")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("consignee_address")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("consignee_phone")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("delivery_fee")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("drug_image")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("drug_introduction")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("drug_number")
                        .HasColumnType("int");

                    b.Property<string>("drug_specification")
                        .HasColumnType("longtext");

                    b.Property<int>("logistics_company")
                        .HasColumnType("int");

                    b.Property<string>("logistics_number")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("order_status")
                        .HasColumnType("int");

                    b.Property<DateTime>("order_time")
                        .HasColumnType("datetime(6)");

                    b.Property<decimal?>("payable_amount")
                        .HasColumnType("decimal(65,30)");

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

                    b.ToTable("Orderdetail");
                });

            modelBuilder.Entity("Training.Domain.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("account")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
