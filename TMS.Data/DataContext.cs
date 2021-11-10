using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TMS.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //this.Database.EnsureDeleted();
            //this.Database.EnsureCreated();
        }

        public virtual DbSet<Board> Boards { get; set; }
        public virtual DbSet<Progress> Progresses { get; set; }
        public virtual DbSet<TaskModel> TaskModels { get; set; }
        public virtual DbSet<SubTaskModel> SubTaskModels { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskModel>()
                .HasOne(uc => uc.Progress)
                .WithMany(u => u.TaskModels)
                .HasForeignKey(x => x.ProgressId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<SubTaskModel>()
                .HasOne(uc => uc.TaskModel)
                .WithMany(k => k.SubTaskModels)
                .HasForeignKey(x => x.TaskModelId).OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<TaskModel>().HasKey(x => x.TaskId);

            modelBuilder.Entity<TaskModel>()
                .HasOne(s => s.Board)
                .WithMany(g => g.TasksModels)
                .HasForeignKey(x => x.BoardId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
