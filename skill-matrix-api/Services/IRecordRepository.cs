using skill_matrix_api.Entities;

namespace skill_matrix_api.Services
{
    public interface IRecordRepository : ICrudRepository
    {
        Task<int> DeleteRecordAsync(int RecordId);
        Task<Record?> GetRecordAsync(int RecordId);
        Task<IEnumerable<Record>> GetRecordsAsync();
        Task PostRecordAsync(Record record);
        Task PostRangeOfRecords(ICollection<Record> records);
        Task<bool> UserExists(int UserId);
        Task<bool> SkillExists(int SkillId);
        Task<bool> QuestionExists(int QuestionId);
    }
}