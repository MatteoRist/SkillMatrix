using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using skill_matrix_api.DbContexts;
using skill_matrix_api.Entities;

namespace skill_matrix_api.Services
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly MatrixContext _context;

        /// <summary>
        /// Initializes a new instance of the QuestionRepository class.
        /// </summary>
        /// <param name="context">The MatrixContext instance.</param>
        public QuestionRepository(MatrixContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc cref="IQuestionRepository.GetQuestionAsync"/>
        public async Task<Question?> GetQuestionAsync(int QuestionId)
        {
            return await _context.Questions.Where(q => q.QuestionId == QuestionId).FirstOrDefaultAsync();
        }

        /// <inheritdoc cref="IQuestionRepository.GetQuestionsAsync"/>
        public async Task<IEnumerable<Question>> GetQuestionsAsync()
        {
            return await _context.Questions.ToListAsync();
        }

        /// <inheritdoc cref="IQuestionRepository.PostQuestionAsync"/>
        public async Task PostQuestionAsync(Question question)
        {
            await _context.AddAsync<Question>(question);
        }

        /// <inheritdoc cref="IQuestionRepository.DeleteQuestionAsync"/>
        public async Task<int> DeleteQuestionAsync(int QuestionId)
        {
            Question? questionToDelete = await _context.Questions.FirstOrDefaultAsync(s => s.QuestionId == QuestionId);

            if (questionToDelete == null)
            {
                return 1;
            }
            else
            {
                _context.Questions.Remove(questionToDelete);
                return 0;
            }
        }

        /// <inheritdoc cref="IQuestionRepository.SaveChangesAsync"/>
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
