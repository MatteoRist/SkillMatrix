using skill_matrix_api.Entities;

namespace skill_matrix_api.Services
{
    /// <summary>
    /// Represents a repository interface for record-related operations.
    /// </summary>
    public interface IRecordRepository : ICrudRepository
    {
        /// <summary>
        /// Asynchronously deletes a record by its ID.
        /// </summary>
        /// <param name="RecordId">The ID of the record to delete.</param>
        /// <returns>A task representing the asynchronous operation and containing the number of affected records.</returns>
        Task<int> DeleteRecordAsync(int RecordId);

        /// <summary>
        /// Asynchronously retrieves a record by its ID.
        /// </summary>
        /// <param name="RecordId">The ID of the record to retrieve.</param>
        /// <returns>A task representing the asynchronous operation and containing the retrieved record.</returns>
        Task<Record?> GetRecordAsync(int RecordId);

        /// <summary>
        /// Asynchronously retrieves a list of records.
        /// </summary>
        /// <returns>A task representing the asynchronous operation and containing the list of records.</returns>
        Task<IEnumerable<Record>> GetRecordsAsync();

        /// <summary>
        /// Asynchronously creates a new record.
        /// </summary>
        /// <param name="record">The record object to create.</param>
        /// <returns>A task representing the asynchronous operation and containing the created record.</returns>
        Task PostRecordAsync(Record record);

        /// <summary>
        /// Asynchronously creates a range of records.
        /// </summary>
        /// <param name="records">The collection of records to create.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task PostRangeOfRecords(ICollection<Record> records);

        /// <summary>
        /// Asynchronously checks if a user with the specified ID exists.
        /// </summary>
        /// <param name="UserId">The ID of the user to check.</param>
        /// <returns>A task representing the asynchronous operation and containing a boolean indicating if the user exists.</returns>
        Task<bool> UserExists(int UserId);


        /// <summary>
        /// Asynchronously checks if a skill with the specified ID exists.
        /// </summary>
        /// <param name="SkillId">The ID of the skill to check.</param>
        /// <returns>A task representing the asynchronous operation and containing a boolean indicating if the skill exists.</returns>
        Task<bool> SkillExists(int SkillId);

        /// <summary>
        /// Asynchronously checks if a question with the specified ID exists.
        /// </summary>
        /// <param name="QuestionId">The ID of the question to check.</param>
        /// <returns>A task representing the asynchronous operation and containing a boolean indicating if the question exists.</returns>
        Task<bool> QuestionExists(int QuestionId);
    }
}