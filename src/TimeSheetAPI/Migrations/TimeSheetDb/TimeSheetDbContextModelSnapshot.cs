﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimeSheetAPI.Models;

namespace TimeSheetAPI.Migrations.TimeSheetDb
{
    [DbContext(typeof(TimeSheetDbContext))]
    partial class TimeSheetDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TimeSheetAPI.Models.UserLogin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("LoginTime")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("LogoutTime")
                        .HasColumnType("datetime");

                    b.Property<string>("UUId")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("PrimaryKey_UserLoginId");

                    b.ToTable("UserLogins");
                });
#pragma warning restore 612, 618
        }
    }
}