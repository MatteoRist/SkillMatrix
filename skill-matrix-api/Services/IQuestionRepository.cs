using skill_matrix_api.Entities;

namespace skill_matrix_api.Services
{
    public interface IQuestionRepository : ICrudRepository
    {
        Task<int> DeleteQuestionAsync(int QuestionId);
        Task<Question?> GetQuestionAsync(int QuestionId);
        Task<IEnumerable<Question>> GetQuestionsAsync();
        Task<Question> PostQuestionAsync(Question question);
    }
}