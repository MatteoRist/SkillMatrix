using Microsoft.AspNetCore.Mvc;
using skill_matrix_api.Entities;
using skill_matrix_api.Services;

namespace skill_matrix_api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/questions")]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionRepository _dataStore;

        public QuestionsController(IQuestionRepository dataStore)
        {
            _dataStore = dataStore;
        }

        /// <summary>
        /// Retrieves a list of questions.
        /// </summary>
        /// <returns>An action result containing the list of questions.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
        {
            return Ok(await _dataStore.GetQuestionsAsync());
        }

        /// <summary>
        /// Retrieves a specific question by its ID.
        /// </summary>
        /// <param name="QuestionId">The ID of the question to retrieve.</param>
        /// <returns>An action result containing the retrieved question.</returns>
        [HttpGet("{QuestionId}", Name = "GetQuestion")]
        public async Task<ActionResult<Question>> GetQuestion(int QuestionId)
        {
            var questionToReturn = await _dataStore.GetQuestionAsync(QuestionId);

            if (questionToReturn == null) { return NotFound(); }

            return Ok(questionToReturn);
        }

        /// <summary>
        /// Creates a new question.
        /// </summary>
        /// <param name="question">The question object to create.</param>
        /// <returns>An action result containing the created question.</returns
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

        /// <summary>
        /// Deletes a specific question by its ID.
        /// </summary>
        /// <param name="QuestionId">The ID of the question to delete.</param>
        /// <returns>An action result indicating the status of the deletion.</returns>
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
