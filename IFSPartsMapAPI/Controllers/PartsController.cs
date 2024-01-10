using Microsoft.AspNetCore.Mvc;
using IFSPartsMapAPI.Models;
using IFSPartsMapAPI.Data; // Add this line if not already there
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace IFSPartsMapAPI.Controllers
{
    [ApiController]
    [Route("api/patient/{patientId}/parts")]
    public class PartsController : ControllerBase
    {
        private readonly IFSPartsMapDbContext _context;

        public PartsController(IFSPartsMapDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public ActionResult<IEnumerable<IFSPart>> GetParts(int patientId)
        {
            var parts = _context.Parts
                                .Where(x => x.PatientId == patientId)
                                .Include(p => p.QuestionResponses)
                                .ToList();
            if (!parts.Any())
            {
                return NotFound($"No parts found for patient ID {patientId}.");
            }
            return Ok(parts);
        }

        [HttpGet("{partId}")]
        public ActionResult<IFSPart> GetPart(int patientId, int partId)
        {
            var partToReturn = _context.Parts.FirstOrDefault(x => x.Id == partId && x.PatientId == patientId);

            if (partToReturn == null)
            {
                return NotFound($"Part with ID {partId} not found for patient ID {patientId}.");
            }
            return Ok(partToReturn);
        }

        [HttpPost]
        public ActionResult<IFSPart> CreatePart(int patientId, IFSPart part)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.PatientId == patientId);
            if (patient == null)
            {
                return NotFound($"Patient with ID {patientId} not found.");
            }

            part.PatientId = patientId;
            _context.Parts.Add(part);
            patient.IFSParts.Add(part);

            return CreatedAtAction(nameof(GetPart), new { patientId = patientId, partId = part.Id }, part);
        }

        [HttpPut("{partId}")]
        public IActionResult UpdatePart(int patientId, int partId, IFSPart updatedPart)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.PatientId == patientId);
            if (patient == null)
            {
                return NotFound($"Patient with ID {patientId} not found.");
            }

            var part = patient.IFSParts.FirstOrDefault(p => p.Id == partId);
            if (part == null)
            {
                return NotFound($"Part with ID {partId} not found for patient ID {patientId}.");
            }

            // Update part details. Consider using AutoMapper in a real application for cleaner code.
            part.PartName = updatedPart.PartName;
            part.PartCategory = updatedPart.PartCategory;
            part.QuestionResponses = updatedPart.QuestionResponses;

            return NoContent();
        }

        [HttpDelete("{partId}")]
        public IActionResult DeletePart(int patientId, int partId)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.PatientId == patientId);
            if (patient == null)
            {
                return NotFound($"Patient with ID {patientId} not found.");
            }

            var part = patient.IFSParts.FirstOrDefault(p => p.Id == partId);
            if (part == null)
            {
                return NotFound($"Part with ID {partId} not found for patient ID {patientId}.");
            }

            patient.IFSParts.Remove(part); // Remove from patient's list
            _context.Parts.Remove(part); // Remove from the global list

            return NoContent();
        }





    }
}
