using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using skill_matrix_api.Entities;
using skill_matrix_api.Models;
using skill_matrix_api.Services;

namespace Record_matrix_api.Controllers
{
    [ApiController]
    [Route("api/records")]
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecordGetDto>>> GetRecords()
        {
            return Ok(_mapper.Map<IEnumerable<RecordGetDto>>(await _dataStore.GetRecordsAsync()));
        }

        [HttpGet("{RecordId}", Name = "GetRecord")]
        public async Task<ActionResult<RecordGetDto>> GetRecord(int RecordId)
        {
            var RecordToReturn = await _dataStore.GetRecordAsync(RecordId);

            if (RecordToReturn == null) { return NotFound(); }

            return Ok(_mapper.Map<RecordGetDto>(RecordToReturn));
        }

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

            var finalRecord = _mapper.Map<Record>(record);

            await _dataStore.PostRecordAsync(finalRecord);

            await _dataStore.SaveChangesAsync();

            var createdRecord = _mapper.Map<RecordGetDto>(finalRecord);

            return CreatedAtRoute("GetRecord",
                new { RecordId = createdRecord.RecordId },
                createdRecord
            );
        }

        [HttpPost("bulk")]
        public async Task<ActionResult<RecordGetDto>> BulkPostRecords([FromBody] List<RecordPostDto> records)
        {
            List<Record> finalData = _mapper.Map<List<Record>>(records);

            for (int i = 0; i < finalData.Count; i++)
            {
                var record  = finalData[i];

                if (
                    !await _dataStore.UserExists(record.UserId) || 
                    !await _dataStore.SkillExists(record.SkillId) ||
                    !await _dataStore.QuestionExists(record.QuestionId)
                    )
                {
                    return BadRequest(new { 
                        message = 
                        $"One or more of the provided IDs do not exist in the database",
                        record
                    });
                }
            }

            foreach (var record in finalData)
            {
                var userExists = await _dataStore.UserExists(record.UserId);
                var skillExists = await _dataStore.SkillExists(record.SkillId);
                var questionExists = await _dataStore.QuestionExists(record.QuestionId);

                if (!userExists || !skillExists || !questionExists)
                    return BadRequest(new { message = "One or more of the provided IDs do not exist in the database. Check record " });
            }

            await _dataStore.PostRangeOfRecords(finalData);

            await _dataStore.SaveChangesAsync();

            return NoContent();
        }


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
