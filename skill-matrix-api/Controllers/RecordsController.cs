using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using skill_matrix_api.Entities;
using skill_matrix_api.Models;
using skill_matrix_api.Services;

namespace Record_matrix_api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/records")]
    public class RecordsController : ControllerBase
    {
        private readonly IRecordRepository _dataStore;
        private readonly IMapper _mapper;

        public RecordsController(IRecordRepository dataStore, IMapper mapper)
        {
            _dataStore = dataStore ??
                throw new ArgumentNullException(nameof(dataStore));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Retrieves a list of records.
        /// </summary>
        /// <returns>An action result containing the list of records.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecordGetDto>>> GetRecords()
        {
            return Ok(_mapper.Map<IEnumerable<RecordGetDto>>(await _dataStore.GetRecordsAsync()));
        }

        /// <summary>
        /// Retrieves a specific record by its ID.
        /// </summary>
        /// <param name="RecordId">The ID of the record to retrieve.</param>
        /// <returns>An action result containing the retrieved record.</returns>
        [HttpGet("{RecordId}", Name = "GetRecord")]
        public async Task<ActionResult<RecordGetDto>> GetRecord(int RecordId)
        {
            var RecordToReturn = await _dataStore.GetRecordAsync(RecordId);

            if (RecordToReturn == null) { return NotFound(); }

            return Ok(_mapper.Map<RecordGetDto>(RecordToReturn));
        }

        /// <summary>
        /// Creates a new record.
        /// </summary>
        /// <param name="record">The record object to create.</param>
        /// <returns>An action result containing the created record.</returns>
        [HttpPost]
        public async Task<ActionResult<RecordGetDto>> PostRecord([FromBody] RecordPostDto record)
        {
            if (record == null)
                throw new ArgumentNullException(nameof(record));

            // Check if User, Skill, and Question exist
            var userExists = await _dataStore.UserExists(record.UserId);
            var skillExists = await _dataStore.SkillExists(record.SkillId);
            var questionExists = await _dataStore.QuestionExists(record.QuestionId);

            if (!userExists || !skillExists || !questionExists)
                return BadRequest(new { message = "One or more of the provided IDs do not exist in the database." });

            Record finalRecord = _mapper.Map<Record>(record);

            // Check if value is inside limits
            finalRecord.Question = null!; //tell compiler Question is not null

            if (finalRecord.Question.MinValue > finalRecord.value ||
                finalRecord.Question.MaxValue < finalRecord.value)
                return BadRequest(new { message = "Value is outside question limits" });

            await _dataStore.PostRecordAsync(finalRecord);

            await _dataStore.SaveChangesAsync();

            var createdRecord = _mapper.Map<RecordGetDto>(finalRecord);

            return CreatedAtRoute("GetRecord",
                new { RecordId = createdRecord.RecordId },
                createdRecord
            );
        }


        /// <summary>
        /// Creates multiple records in bulk.
        /// </summary>
        /// <param name="records">The list of record objects to create.</param>
        /// <returns>An action result indicating the status of the bulk creation.</returns>
        [HttpPost("bulk")]
        public async Task<ActionResult<RecordPostDto>> BulkPostRecords([FromBody] List<RecordPostDto> records)
        {
            List<Record> finalData = _mapper.Map<List<Record>>(records);

            for (int i = 0; i < finalData.Count; i++)
            {
                var record  = finalData[i];
                record.Question = null!; //tell compiler Question is not null

                if (
                    !await _dataStore.UserExists(record.UserId) || 
                    !await _dataStore.SkillExists(record.SkillId) ||
                    !await _dataStore.QuestionExists(record.QuestionId)
                    )
                {
                    RecordPostDto responseObject = _mapper.Map<RecordPostDto>(record);
                    return BadRequest(new { 
                        message = 
                        "One or more of the provided IDs do not exist in the database",
                        responseObject
                    });
                }
                // Check if value is inside limits
                else if (record.Question.MinValue > record.value ||
                    record.Question.MaxValue < record.value) 
                    return BadRequest(new { message = "Value is outside question limits" });
            }

            await _dataStore.PostRangeOfRecordsAsync(finalData);

            await _dataStore.SaveChangesAsync();

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
            if (await _dataStore.DeleteRecordAsync(RecordId) != 0)
                return BadRequest(new { message = "The data your tring to delete does not exist" });

            await _dataStore.SaveChangesAsync();

            return NoContent();
        }
    }
}
