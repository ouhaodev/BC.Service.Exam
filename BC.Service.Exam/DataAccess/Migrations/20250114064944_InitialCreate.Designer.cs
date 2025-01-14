﻿// <auto-generated />
using System;
using BC.Service.Exam.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BC.Service.Exam.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250114064944_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BC.Service.Exam.Domain.Candidate", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    b.Property<int>("Age")
                        .HasColumnType("int")
                        .HasColumnName("age");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("createdBy");

                    b.Property<DateTime>("CreatedDataTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("createdDataTime");

                    b.Property<string>("LastModifiedBy")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("lastModifiedBy");

                    b.Property<DateTime?>("LastModifiedDataTime")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("lastModifiedDataTime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("bc_candidate", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}