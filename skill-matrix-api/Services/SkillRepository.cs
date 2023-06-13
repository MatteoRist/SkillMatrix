using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using skill_matrix_api.DbContexts;
using skill_matrix_api.Entities;

namespace skill_matrix_api.Services
{
    public class SkillRepository : ISkillRepository
    {
        private readonly MatrixContext _context;

        /// <summary>
        /// Initializes a new instance of the SkillRepository class.
        /// </summary>
        /// <param name="context">The MatrixContext instance.</param>
        public SkillRepository(MatrixContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc cref="ISkillRepository.GetSkillsAsync"/>
        public async Task<IEnumerable<Skill>> GetSkillsAsync()
        {
            return await _context.Skills.ToListAsync();
        }

        /// <inheritdoc cref="ISkillRepository.GetSkillAsync"/>
        public async Task<Skill?> GetSkillAsync(int SkillId)
        {
            return await _context.Skills.Where(c => c.SkillId == SkillId).FirstOrDefaultAsync();
        }

        /// <inheritdoc cref="ISkillRepository.PostSkillAsync"/>
        public async Task PostSkillAsync(Skill skill)
        {
            await _context.AddAsync<Skill>(skill);
        }

        /// <inheritdoc cref="ISkillRepository.DeleteSkillAsync"/>
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

        /// <inheritdoc cref="ISkillRepository.SaveChangesAsync"/>
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        /// <inheritdoc cref="IRecordRepository.PostRangeOfRecordsAsync"/>
        public async Task PostRangeOfSkillsAsync(ICollection<Skill> skills)
        {
            await _context.Skills.AddRangeAsync(skills);
        }
    }
}
