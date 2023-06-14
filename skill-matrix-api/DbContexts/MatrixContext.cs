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
        public DbSet<Category> Category { get; set; }

        public MatrixContext(DbContextOptions<MatrixContext> options) : base(options) 
        {  

        }
    }
}
