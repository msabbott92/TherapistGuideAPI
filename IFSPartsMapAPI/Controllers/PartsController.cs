using Microsoft.AspNetCore.Mvc;
using IFSPartsMapAPI.Models;
using System.Linq;

namespace IFSPartsMapAPI.Controllers
{
    [ApiController]
    [Route("api/patient/{patientId}/parts")]
    public class PartsController : ControllerBase
    {
        private readonly IFSPartsMap _iFSPartsMap;

        public PartsController(IFSPartsMap iFSPartsMap)
        {
            _iFSPartsMap = iFSPartsMap ?? throw new ArgumentNullException(nameof(iFSPartsMap));
        }

        [HttpGet]
        public ActionResult<IEnumerable<IFSPart>> GetParts(int patientId)
        {
            var parts = _iFSPartsMap.Parts.Where(x => x.PatientId == patientId).ToList();
            if (!parts.Any())
            {
                return NotFound($"No parts found for patient ID {patientId}.");
            }
            return Ok(parts);
        }

        [HttpGet("{partId}")]
        public ActionResult<IFSPart> GetPart(int patientId, int partId)
        {
            var partToReturn = _iFSPartsMap.Parts.FirstOrDefault(x => x.Id == partId && x.PatientId == patientId);

            if (partToReturn == null)
            {
                return NotFound($"Part with ID {partId} not found for patient ID {patientId}.");
            }
            return Ok(partToReturn);
        }

        [HttpPost]
        public ActionResult<IFSPart> CreatePart(int patientId, IFSPart part)
        {
            var patient = _iFSPartsMap.Patients.FirstOrDefault(p => p.PatientId == patientId);
            if (patient == null)
            {
                return NotFound($"Patient with ID {patientId} not found.");
            }

            part.PatientId = patientId;
            _iFSPartsMap.Parts.Add(part);
            patient.IFSParts.Add(part);

            return CreatedAtAction(nameof(GetPart), new { patientId = patientId, partId = part.Id }, part);
        }

        [HttpPut("{partId}")]
        public IActionResult UpdatePart(int patientId, int partId, IFSPart updatedPart)
        {
            var patient = _iFSPartsMap.Patients.FirstOrDefault(p => p.PatientId == patientId);
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
            var patient = _iFSPartsMap.Patients.FirstOrDefault(p => p.PatientId == patientId);
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
            _iFSPartsMap.Parts.Remove(part); // Remove from the global list

            return NoContent();
        }





    }
}
