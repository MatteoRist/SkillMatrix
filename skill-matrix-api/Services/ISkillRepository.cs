using skill_matrix_api.Entities;

namespace skill_matrix_api.Services
{
    /// <summary>
    /// Represents a repository interface for skill-related operations.
    /// </summary>
    public interface ISkillRepository : ICrudRepository
    {
        /// <summary>
        /// Asynchronously deletes a skill by its ID.
        /// </summary>
        /// <param name="SkillId">The ID of the skill to delete.</param>
        /// <returns>A task representing the asynchronous operation and containing the number of affected records.</returns>
        Task<int> DeleteSkillAsync(int SkillId);

        /// <summary>
        /// Asynchronously retrieves a skill by its ID.
        /// </summary>
        /// <param name="SkillId">The ID of the skill to retrieve.</param>
        /// <returns>A task representing the asynchronous operation and containing the retrieved skill.</returns>
        Task<Skill?> GetSkillAsync(int SkillId);

        /// <summary>
        /// Asynchronously retrieves a list of skills.
        /// </summary>
        /// <returns>A task representing the asynchronous operation and containing the list of skills.</returns>
        Task<IEnumerable<Skill>> GetSkillsAsync();

        /// <summary>
        /// Asynchronously creates a new skill.
        /// </summary>
        /// <param name="skill">The skill object to create.</param>
        /// <returns>A task representing the asynchronous operation and containing the created skill.</returns>
        Task PostSkillAsync(Skill skill);

        /// <summary>
        /// Asynchronously creates a range of skills.
        /// </summary>
        /// <param name="skills">The collection of skills to create.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task PostRangeOfSkillsAsync(ICollection<Skill> skills);
    }
}