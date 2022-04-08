﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sisma.Project1.DAL.Data;

#nullable disable

namespace Sisma.Project1.DAL.Migrations
{
    [DbContext(typeof(SismaContext))]
    [Migration("20220408151605_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Sisma.Project1.DAL.Data.Class", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RecordCreateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RefId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SchoolId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RefId")
                        .IsUnique();

                    b.HasIndex("SchoolId");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("Sisma.Project1.DAL.Data.School", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RecordCreateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RefId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RefId")
                        .IsUnique();

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("Sisma.Project1.DAL.Data.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RecordCreateDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("RefId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SchoolId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RefId")
                        .IsUnique();

                    b.HasIndex("SchoolId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Sisma.Project1.DAL.Data.StudentInClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ClassId")
                        .HasColumnType("int");

                    b.Property<Guid>("RefId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RefId")
                        .IsUnique();

                    b.HasIndex("StudentId");

                    b.HasIndex("ClassId", "StudentId")
                        .IsUnique();

                    b.ToTable("StudentInClasses");
                });

            modelBuilder.Entity("Sisma.Project1.DAL.Data.Class", b =>
                {
                    b.HasOne("Sisma.Project1.DAL.Data.School", "School")
                        .WithMany("Classes")
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("School");
                });

            modelBuilder.Entity("Sisma.Project1.DAL.Data.Student", b =>
                {
                    b.HasOne("Sisma.Project1.DAL.Data.School", "School")
                        .WithMany("Students")
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("School");
                });

            modelBuilder.Entity("Sisma.Project1.DAL.Data.StudentInClass", b =>
                {
                    b.HasOne("Sisma.Project1.DAL.Data.Class", "Class")
                        .WithMany("StudentInClasses")
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Sisma.Project1.DAL.Data.Student", "Student")
                        .WithMany("StudentInClasses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Sisma.Project1.DAL.Data.Class", b =>
                {
                    b.Navigation("StudentInClasses");
                });

            modelBuilder.Entity("Sisma.Project1.DAL.Data.School", b =>
                {
                    b.Navigation("Classes");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("Sisma.Project1.DAL.Data.Student", b =>
                {
                    b.Navigation("StudentInClasses");
                });
#pragma warning restore 612, 618
        }
    }
}