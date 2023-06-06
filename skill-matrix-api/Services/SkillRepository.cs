using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using skill_matrix_api.DbContexts;
using skill_matrix_api.Entities;

namespace skill_matrix_api.Services
{
    public class SkillRepository : ISkillRepository
    {
        private readonly MatrixContext _context;

        public SkillRepository(MatrixContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Skill>> GetSkillsAsync()
        {
            return await _context.Skills.ToListAsync();
        }

        public async Task<Skill?> GetSkillAsync(int SkillId)
        {
            return await _context.Skills.Where(c => c.SkillId == SkillId).FirstOrDefaultAsync();
        }

        public async Task PostSkillAsync(Skill skill)
        {
            await _context.AddAsync<Skill>(skill);
        }

        public async Task<int> DeleteSkillAsync(int SkillId)
        {
            Skill? skillToDelete = await _context.Skills.FirstOrDefaultAsync(s => s.SkillId == SkillId);

            if (skillToDelete == null)
            {
                return 1;
            }
            else
            {
                _context.Skills.Remove(skillToDelete);
                return 0;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
