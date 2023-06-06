using Microsoft.AspNetCore.Mvc;
using skill_matrix_api.Entities;
using skill_matrix_api.Services;

namespace skill_matrix_api.Controllers
{
    [ApiController]
    [Route("api/questions")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionRepository _dataStore;

        public QuestionsController(IQuestionRepository dataStore)
        {
            _dataStore = dataStore;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
        {
            return Ok(await _dataStore.GetQuestionsAsync());
        }

        [HttpGet("{QuestionId}", Name = "GetQuestion")]
        public async Task<ActionResult<Question>> GetQuestion(int QuestionId)
        {
            var questionToReturn = await _dataStore.GetQuestionAsync(QuestionId);

            if (questionToReturn == null) { return NotFound(); }

            return Ok(questionToReturn);
        }

        [HttpPost]
        public async Task<ActionResult<Question>> PostQuestion([FromBody] Question question)
        {
            if (question == null)
                throw new ArgumentNullException(nameof(question));

            await _dataStore.PostQuestionAsync(question);

            await _dataStore.SaveChangesAsync();

            return CreatedAtRoute("GetQuestion",
                new { QuestionId = question.QuestionId },
                question
            );
        }

        [HttpDelete("{QuestionId}")]
        public async Task<ActionResult> DeleteSkill(int QuestionId)
        {
            if (await _dataStore.DeleteQuestionAsync(QuestionId) != 0)
                return BadRequest(new { message = "The data your tring to delete does not exist" });
           
            await _dataStore.SaveChangesAsync();

            return NoContent();
        }
    }
}
