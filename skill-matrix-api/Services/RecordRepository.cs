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
            return await _context.Records.Where(q => q.RecordId == RecordId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Record>> GetRecordsAsync()
        {
            return await _context.Records.ToListAsync();
        }

        public async Task<Record> PostRecordAsync(Record record)
        {
            EntityEntry<Record> entityEntry;

            Record? oldRecord = await _context.Records.FirstOrDefaultAsync(s => s.RecordId == record.RecordId);

            if (oldRecord == null)
            {
                entityEntry = _context.Add<Record>(record);
            }
            else
            {
                entityEntry = _context.Update<Record>(record);
            }

            return entityEntry.Entity;
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

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
