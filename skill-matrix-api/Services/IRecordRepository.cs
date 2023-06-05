using skill_matrix_api.Entities;

namespace skill_matrix_api.Services
{
    public interface IRecordRepository : ICrudRepository
    {
        Task<int> DeleteRecordAsync(int RecordId);
        Task<Record?> GetRecordAsync(int RecordId);
        Task<IEnumerable<Record>> GetRecordsAsync();
        Task<Record> PostRecordAsync(Record record);
    }
}