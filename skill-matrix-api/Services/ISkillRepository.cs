using skill_matrix_api.Entities;

namespace skill_matrix_api.Services
{
    public interface ISkillRepository : ICrudRepository
    {
        Task<int> DeleteSkillAsync(int SkillId);
        Task<Skill?> GetSkillAsync(int SkillId);
        Task<IEnumerable<Skill>> GetSkillsAsync();
        Task<Skill> PostSkillAsync(Skill skill);
    }
}