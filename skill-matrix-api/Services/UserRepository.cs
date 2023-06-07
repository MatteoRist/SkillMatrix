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

    }
}
