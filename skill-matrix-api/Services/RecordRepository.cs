using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using skill_matrix_api.DbContexts;
using skill_matrix_api.Entities;

namespace skill_matrix_api.Services
{
    public class RecordRepository : IRecordRepository
    {
        private readonly MatrixContext _context;

        /// <summary>
        /// Initializes a new instance of the RecordRepository class.
        /// </summary>
        /// <param name="context">The MatrixContext instance.</param>
        public RecordRepository(MatrixContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc cref="IRecordRepository.GetRecordAsync"/>
        public async Task<Record?> GetRecordAsync(int RecordId)
        {
            return await _context.Records
                .FirstOrDefaultAsync(q => q.RecordId == RecordId);
        }

        /// <inheritdoc cref="IRecordRepository.GetRecordsAsync"/>
        public async Task<IEnumerable<Record>> GetRecordsAsync()
        {
            return await _context.Records
                .ToListAsync();
        }

        /// <inheritdoc cref="IRecordRepository.GetRecordsAsync"/>
        public async Task<IEnumerable<Record>> GetRecordsAsync(int UserId)
        {
            if (UserId > 0)
            {
                return await _context.Records
                .Where(r => r.UserId == UserId)
                .ToListAsync();
            }
            return await GetRecordsAsync();
        }

        /// <inheritdoc cref="IRecordRepository.PostRecordAsync"/>
        public async Task PostRecordAsync(Record record)
        {
            await _context.Records.AddAsync(record);
        }

        /// <inheritdoc cref="IRecordRepository.PostRangeOfRecordsAsync"/>
        public async Task PostRangeOfRecordsAsync(ICollection<Record> records)
        {
            await _context.Records.AddRangeAsync(records);
        }

        /// <inheritdoc cref="IRecordRepository.DeleteRecordAsync"/>
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

        /// <inheritdoc cref="IRecordRepository.SaveChangesAsync"/>
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
