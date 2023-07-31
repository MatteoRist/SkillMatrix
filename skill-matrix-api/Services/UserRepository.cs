using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using skill_matrix_api.DbContexts;
using skill_matrix_api.Entities;

namespace skill_matrix_api.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly MatrixContext _context;

        /// <summary>
        /// Initializes a new instance of the UserRepository class.
        /// </summary>
        /// <param name="context">The MatrixContext instance.</param>
        public UserRepository(MatrixContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc cref="IUserRepository.GetUsersAsync"/>
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        /// <inheritdoc cref="IUserRepository.GetUserAsync"/>
        public async Task<User?> GetUserAsync(int UserId)
        {
            return await _context.Users.Where(c => c.UserId == UserId).FirstOrDefaultAsync();
        }

        /// <inheritdoc cref="IUserRepository.GetUserAsync"/>
        public async Task<IEnumerable<Statistic>> GetUserStatisticsAsync(int userId)
        {
            var userStatistics = await _context.Statistics
                .FromSqlRaw(@"
            SELECT R.UserId, C.CategoryId, C.Name as CategoryName, AVG(Value * (@MaxCharValue / MaxValue)) as StatValue
            FROM Records R
            INNER JOIN Questions Q ON Q.QuestionId = R.QuestionId
            INNER JOIN Skills S ON S.SkillId = R.SkillId
            INNER JOIN Category C ON C.CategoryId = S.CategoryId
            INNER JOIN Users U ON U.UserId = R.UserId
            WHERE R.UserId = @UserId
            GROUP BY R.UserId, C.CategoryId, C.Name",
                    new SqlParameter("@UserId", userId),
                    new SqlParameter("@MaxCharValue", 100))
                .ToListAsync();

            return userStatistics;
        }
    }
}
