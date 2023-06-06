using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using skill_matrix_api.DbContexts;
using skill_matrix_api.Entities;

namespace skill_matrix_api.Services
{
    public class RecordRepository : IRecordRepository
    {
        private readonly MatrixContext _context;

        public RecordRepository(MatrixContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Record?> GetRecordAsync(int RecordId)
        {
            return await _context.Records
                .Include(r => r.User)
                .Include(r => r.Skill)
                .Include(r => r.Question)
                .FirstOrDefaultAsync(q => q.RecordId == RecordId);
        }

        public async Task<IEnumerable<Record>> GetRecordsAsync()
        {
            return await _context.Records
                .Include(r => r.User)
                .Include(r => r.Skill)
                .Include(r => r.Question)
                .ToListAsync();
        }

        public async Task PostRecordAsync(Record record)
        {
            await _context.Records.AddAsync(record);
        }

        public async Task PostRangeOfRecords(ICollection<Record> records)
        {
            await _context.Records.AddRangeAsync(records);
        }

        public async Task<int> DeleteRecordAsync(int RecordId)
        {
            Record? recordToDelete = await _context.Records.FirstOrDefaultAsync(s => s.RecordId == RecordId);

            if (recordToDelete == null)
            {
                return 1;
            }
            else
            {
                _context.Records.Remove(recordToDelete);
                return 0;
            }
        }

        public async Task<bool> UserExists(int UserId)
        {
            return await _context.Users.AnyAsync(u => u.UserId == UserId);
        }

        public async Task<bool> SkillExists(int SkillId)
        {
            return await _context.Skills.AnyAsync(s => s.SkillId == SkillId);
        }

        public async Task<bool> QuestionExists(int QuestionId)
        {
            return await _context.Questions.AnyAsync(q => q.QuestionId == QuestionId);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
