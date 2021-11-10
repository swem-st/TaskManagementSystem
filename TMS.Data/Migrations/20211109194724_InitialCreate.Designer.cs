﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TMS.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20211109194724_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TMS.Data.Entities.Board", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Boards");
                });

            modelBuilder.Entity("TMS.Data.Entities.Progress", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ProgressName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Progresses");
                });

            modelBuilder.Entity("TMS.Data.Entities.SubTaskModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Done")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TaskModelId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TaskModelId");

                    b.ToTable("SubTaskModels");
                });

            modelBuilder.Entity("TMS.Data.Entities.TaskModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BoardId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FinishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProgressId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BoardId");

                    b.HasIndex("ProgressId");

                    b.ToTable("TaskModels");
                });

            modelBuilder.Entity("TMS.Data.Entities.SubTaskModel", b =>
                {
                    b.HasOne("TMS.Data.Entities.TaskModel", "TaskModel")
                        .WithMany("SubTaskModels")
                        .HasForeignKey("TaskModelId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("TaskModel");
                });

            modelBuilder.Entity("TMS.Data.Entities.TaskModel", b =>
                {
                    b.HasOne("TMS.Data.Entities.Board", "Board")
                        .WithMany("TasksModels")
                        .HasForeignKey("BoardId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("TMS.Data.Entities.Progress", "Progress")
                        .WithMany("TaskModels")
                        .HasForeignKey("ProgressId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Board");

                    b.Navigation("Progress");
                });

            modelBuilder.Entity("TMS.Data.Entities.Board", b =>
                {
                    b.Navigation("TasksModels");
                });

            modelBuilder.Entity("TMS.Data.Entities.Progress", b =>
                {
                    b.Navigation("TaskModels");
                });

            modelBuilder.Entity("TMS.Data.Entities.TaskModel", b =>
                {
                    b.Navigation("SubTaskModels");
                });
#pragma warning restore 612, 618
        }
    }
}
