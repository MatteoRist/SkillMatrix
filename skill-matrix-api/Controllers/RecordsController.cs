using Microsoft.AspNetCore.Mvc;
using skill_matrix_api.Entities;
using skill_matrix_api.Services;

namespace Record_matrix_api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/records")]
    public class RecordsController : ControllerBase
    {
        private readonly IRecordRepository _recordRepo;
        private readonly IUserRepository _userRepo;
        private readonly ISkillRepository _skillRepo;
        private readonly IQuestionRepository _questionRepo;

        public RecordsController(IRecordRepository recordRepo, 
            IUserRepository userRepo, 
            ISkillRepository skillRepo,
            IQuestionRepository questionRepo
            )
        {
            _recordRepo = recordRepo ??
                throw new ArgumentNullException(nameof(recordRepo));
            _userRepo = userRepo ??
                throw new ArgumentNullException(nameof(userRepo));
            _skillRepo = skillRepo ??
                throw new ArgumentNullException(nameof(skillRepo));
            _questionRepo = questionRepo ??
                throw new ArgumentNullException(nameof(questionRepo));
        }

        /// <summary>
        /// Retrieves a list of records.
        /// </summary>
        /// <returns>An action result containing the list of records.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Record>>> GetRecords([FromQuery] int UserId)
        {
            // get records
            IEnumerable<Record> records = await _recordRepo.GetRecordsAsync(UserId);

            return Ok(records);
        }

        /// <summary>
        /// Retrieves a specific record by its ID.
        /// </summary>
        /// <param name="RecordId">The ID of the record to retrieve.</param>
        /// <returns>An action result containing the retrieved record.</returns>
        [HttpGet("{RecordId}", Name = "GetRecord")]
        public async Task<ActionResult<Record>> GetRecord(int RecordId)
        {
            // get record
            var record = await _recordRepo.GetRecordAsync(RecordId);

            // check record
            if (record == null) { return NotFound(); }

            return Ok(record);
        }

        /// <summary>
        /// Creates a new record.
        /// </summary>
        /// <param name="record">The record object to create.</param>
        /// <returns>An action result containing the created record.</returns>
        [HttpPost]
        public async Task<ActionResult<Record>> PostRecord([FromBody] Record record)
        {
            if (record == null)
                throw new ArgumentNullException(nameof(record));

            string? badRequestMessage = null;
            if (await _userRepo.GetUserAsync(record.UserId) == null)
            {
                badRequestMessage = "The userId provided does not exist";
            }

            if (await _skillRepo.GetSkillAsync(record.SkillId) == null)
            {
                badRequestMessage = "The SkillId provided does not exist";
            }

            Question? question = await _questionRepo.GetQuestionAsync(record.QuestionId);
            if (question == null)
            {
                badRequestMessage = "The QuestionId provided does not exist";
            }
            else if (question.MinValue > record.Value ||
                question.MaxValue < record.Value)
            {
                badRequestMessage = "Value is outside question limits";
            }

            if (badRequestMessage != null)
                return BadRequest(new
                {
                    message = badRequestMessage,
                    record
                });

            await _recordRepo.PostRecordAsync(record);

            await _recordRepo.SaveChangesAsync();

            return CreatedAtRoute("GetRecord",
                new { RecordId = record.RecordId },
                record
            );
        }


        /// <summary>
        /// Creates multiple records in bulk.
        /// </summary>
        /// <param name="records">The list of record objects to create.</param>
        /// <returns>An action result indicating the status of the bulk creation.</returns>
        [HttpPost("bulk")]
        public async Task<ActionResult<Record>> BulkPostRecords([FromBody] List<Record> records)
        {
            for (int i = 0; i < records.Count; i++)
            {
                var record  = records[i];

                string? badRequestMessage = null;
                if (await _userRepo.GetUserAsync(record.UserId) == null)
                {
                    badRequestMessage = "The userId provided does not exist";
                }

                if (await _skillRepo.GetSkillAsync(record.SkillId) == null)
                {
                    badRequestMessage = "The SkillId provided does not exist";
                }

                Question? question = await _questionRepo.GetQuestionAsync(record.QuestionId);
                if (question == null)
                {
                    badRequestMessage = "The QuestionId provided does not exist";
                }
                else if (question.MinValue > record.Value ||
                    question.MaxValue < record.Value)
                {
                    badRequestMessage = "Value is outside question limits";
                }

                if (badRequestMessage != null)
                    return BadRequest(new
                    {
                        message = badRequestMessage,
                        record
                    });
            }

            await _recordRepo.PostRangeOfRecordsAsync(records);

            await _recordRepo.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Deletes a specific record by its ID.
        /// </summary>
        /// <param name="RecordId">The ID of the record to delete.</param>
        /// <returns>An action result indicating the status of the deletion.</returns>
        [HttpDelete("{RecordId}")]
        public async Task<ActionResult> DeleteRecord(int RecordId)
        {
            if (await _recordRepo.DeleteRecordAsync(RecordId) != 0)
                return BadRequest(new { message = "The data your tring to delete does not exist" });

            await _recordRepo.SaveChangesAsync();

            return NoContent();
        }
    }
}
