using IFSPartsMapAPI.Data;
using IFSPartsMapAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IFSPartsMapAPI.Controllers
{
    [Route("api/patient/{patientId}/parts/{partId}/responses")]
    [ApiController]
    public class QuestionResponseController : ControllerBase
    {
        private readonly IFSPartsMapDbContext _context;

        public QuestionResponseController(IFSPartsMapDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public ActionResult<IEnumerable<QuestionResponse>> GetQuestionResponses(int patientId, int partId)
        {
            var part = _context.Parts.FirstOrDefault(p => p.Id == partId && p.PatientId == patientId);
            if (part == null)
            {
                return NotFound($"Part with ID {partId} not found for patient ID {patientId}.");
            }

            var responses = part.QuestionResponses;
            if (responses == null || !responses.Any())
            {
                return NotFound($"No responses found for part ID {partId}.");
            }

            return Ok(responses);
        }


        [HttpGet("{responseId}")]

        public ActionResult<QuestionResponse> GetQuestionResponse(int patientId, int partId, int responseId)
        {
            var part = _context.Parts.FirstOrDefault(p => p.Id == partId && p.PatientId == patientId);
            if (part == null)
            {
                return NotFound($"Part with ID {partId} not found for patient ID {patientId}.");
            }

            var response = part.QuestionResponses.FirstOrDefault(r => r.QuestionResponseId == responseId);
            if (response == null)
            {
                return NotFound($"Response with ID {responseId} not found for part ID {partId}.");
            }

            return Ok(response);
        }

        [HttpPost]
        public ActionResult<QuestionResponse> CreateResponse(int patientId, int partId, QuestionResponse response)
        {
            var part = _context.Parts.FirstOrDefault(p => p.Id == partId && p.PatientId == patientId);
            if (part == null)
            {
                return NotFound($"Part with ID {partId} not found for patient ID {patientId}.");
            }

            part.QuestionResponses.Add(response);
            return CreatedAtAction(nameof(GetQuestionResponse), new { patientId = patientId, partId = partId, responseId = response.QuestionResponseId }, response);
        }


        [HttpPut("{responseId}")]
        public IActionResult UpdateResponse(int patientId, int partId, int responseId, QuestionResponse updatedResponse)
        {
            var part = _context.Parts.FirstOrDefault(p => p.Id == partId && p.PatientId == patientId);
            if (part == null)
            {
                return NotFound($"Part with ID {partId} not found for patient ID {patientId}.");
            }

            var response = part.QuestionResponses.FirstOrDefault(r => r.QuestionResponseId == responseId);
            if (response == null)
            {
                return NotFound($"Response with ID {responseId} not found for part ID {partId}.");
            }

            // Update response details
            response.Question = updatedResponse.Question;
            response.Response = updatedResponse.Response;

            return NoContent();
        }

        [HttpDelete("{responseId}")]
        public IActionResult DeleteResponse(int patientId, int partId, int responseId)
        {
            var part = _context.Parts.FirstOrDefault(p => p.Id == partId && p.PatientId == patientId);
            if (part == null)
            {
                return NotFound($"Part with ID {partId} not found for patient ID {patientId}.");
            }

            var response = part.QuestionResponses.FirstOrDefault(r => r.QuestionResponseId == responseId);
            if (response == null)
            {
                return NotFound($"Response with ID {responseId} not found for part ID {partId}.");
            }

            part.QuestionResponses.Remove(response);
            return NoContent();
        }
    }
}
