using Microsoft.EntityFrameworkCore;
using skill_matrix_api.Entities;

namespace skill_matrix_api.DbContexts
{
    public class MatrixContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Record> Records { get; set; }

        public MatrixContext(DbContextOptions<MatrixContext> options) : base(options) 
        {  
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Record>()
        //        .HasOne(r => r.User)
        //        .WithMany(u => u.Records)
        //        .HasForeignKey(r => r.UserId);

        //    modelBuilder.Entity<Record>()
        //        .HasOne(r => r.Skill)
        //        .WithMany(s => s.Records)
        //        .HasForeignKey(r => r.SkillId);
             
        //    modelBuilder.Entity<Record>()
        //        .HasOne(r => r.Question)
        //        .WithMany(q => q.Records)
        //        .HasForeignKey(r => r.QuestionId);
        //}
    }
}
