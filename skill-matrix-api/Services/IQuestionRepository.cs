using skill_matrix_api.Entities;

namespace skill_matrix_api.Services
{
    /// <summary>
    /// Represents a repository interface for question-related operations.
    /// </summary>
    public interface IQuestionRepository : ICrudRepository
    {
        /// <summary>
        /// Asynchronously deletes a question by its ID.
        /// </summary>
        /// <param name="QuestionId">The ID of the question to delete.</param>
        /// <returns>A task representing the asynchronous operation and containing the number of affected records.</returns>
        Task<int> DeleteQuestionAsync(int QuestionId);

        /// <summary>
        /// Asynchronously retrieves a question by its ID.
        /// </summary>
        /// <param name="QuestionId">The ID of the question to retrieve.</param>
        /// <returns>A task representing the asynchronous operation and containing the retrieved question.</returns>
        Task<Question?> GetQuestionAsync(int QuestionId);

        /// <summary>
        /// Asynchronously retrieves a list of questions.
        /// </summary>
        /// <returns>A task representing the asynchronous operation and containing the list of questions.</returns>
        Task<IEnumerable<Question>> GetQuestionsAsync();

        /// <summary>
        /// Asynchronously creates a new question.
        /// </summary>
        /// <param name="question">The question object to create.</param>
        /// <returns>A task representing the asynchronous operation and containing the created question.</returns>
        Task PostQuestionAsync(Question question);
    }
}