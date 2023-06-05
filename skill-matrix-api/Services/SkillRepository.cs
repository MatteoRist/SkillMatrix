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

        public async Task<Skill> PostSkillAsync(Skill skill)
        {
            EntityEntry<Skill> entityEntry;

            Skill? oldSkill = await _context.Skills.FirstOrDefaultAsync(s => s.SkillId == skill.SkillId);

            if (oldSkill == null)
            {
                entityEntry = _context.Add<Skill>(skill);
            }
            else
            {
                entityEntry = _context.Update<Skill>(skill);
            }

            return entityEntry.Entity;
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
