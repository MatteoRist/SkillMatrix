using Microsoft.AspNetCore.Mvc;
using skill_matrix_api.Entities;
using skill_matrix_api.Services;

namespace Record_matrix_api.Controllers
{
    [ApiController]
    [Route("api/records")]
    public class RecordsController : ControllerBase
    {
        private readonly IRecordRepository _dataStore;

        public RecordsController(IRecordRepository dataStore)
        {
            _dataStore = dataStore;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Record>>> GetRecords()
        {
            return Ok(await _dataStore.GetRecordsAsync());
        }

        [HttpGet("{RecordId}", Name = "GetRecord")]
        public async Task<ActionResult<Record>> GetRecord(int RecordId)
        {
            var RecordToReturn = await _dataStore.GetRecordAsync(RecordId);

            if (RecordToReturn == null) { return NotFound(); }

            return Ok(RecordToReturn);
        }

        [HttpPost]
        public async Task<ActionResult<Record>> PostRecord([FromBody] Record Record)
        {
            if (Record == null)
                throw new ArgumentNullException(nameof(Record));

            Record result = await _dataStore.PostRecordAsync(Record);

            await _dataStore.SaveChangesAsync();

            return CreatedAtRoute("GetRecord",
                new { RecordId = result.RecordId },
                result
            );
        }

        [HttpDelete("{RecordId}")]
        public async Task<ActionResult> DeleteRecord(int RecordId)
        {
            await _dataStore.DeleteRecordAsync(RecordId);

            await _dataStore.SaveChangesAsync();

            return NoContent();
        }
    }
}
