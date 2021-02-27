﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TimeSheetAPI.Models;

namespace TimeSheetAPI.Migrations.TimeSheetDb
{
    [DbContext(typeof(TimeSheetDbContext))]
    [Migration("20210227103200_AddDate")]
    partial class AddDate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("TimeSheetAPI.Models.SubmissionSheet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime");

                    b.Property<TimeSpan>("Login")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("Logout")
                        .HasColumnType("time");

                    b.Property<string>("UUId")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("SubmissionSheets");
                });

            modelBuilder.Entity("TimeSheetAPI.Models.UserLogin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("LoginTime")
                        .HasColumnType("datetime");

                    b.Property<string>("UUId")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("PrimaryKey_UserLoginId");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("TimeSheetAPI.Models.UserLogout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("LogoutTime")
                        .HasColumnType("datetime");

                    b.Property<string>("UUId")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("PrimaryKey_UserLogoutId");

                    b.ToTable("UserLogouts");
                });
#pragma warning restore 612, 618
        }
    }
}