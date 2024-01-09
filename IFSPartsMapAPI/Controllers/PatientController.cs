using Microsoft.AspNetCore.Mvc;
using IFSPartsMapAPI.Models;
using System.Linq;

namespace IFSPartsMapAPI.Controllers
{
    [ApiController]
    [Route("api/patients")]
    public class PatientController : ControllerBase
    {
        private readonly IFSPartsMap _iFSPartsMap;

        public PatientController(IFSPartsMap iFSPartsMap)
        {
            _iFSPartsMap = iFSPartsMap;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Patient>> GetPatients()
        {
            return Ok(_iFSPartsMap.Patients);
        }

        [HttpGet("{id}")]
        public ActionResult<Patient> GetPatient(int id)
        {
            var patient = _iFSPartsMap.Patients.FirstOrDefault(p => p.PatientId == id);

            if (patient == null)
            {
                return NotFound($"Patient with ID {id} not found.");
            }

            return Ok(patient);
        }

        [HttpPost]
        public ActionResult<Patient> CreatePatient(Patient patient)
        {
            // In a real application, you would also need to check for existing patients, validate inputs, etc.
            _iFSPartsMap.Patients.Add(patient);
            return CreatedAtAction(nameof(GetPatient), new { id = patient.PatientId }, patient);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePatient(int id, Patient patient)
        {
            var existingPatient = _iFSPartsMap.Patients.FirstOrDefault(p => p.PatientId == id);

            if (existingPatient == null)
            {
                return NotFound($"Patient with ID {id} not found.");
            }

            // Update properties. In a real application, consider using a patch or a specific update method.
            existingPatient.FirstName = patient.FirstName;
            existingPatient.LastName = patient.LastName;
            existingPatient.Age = patient.Age;
            existingPatient.Gender = patient.Gender;
            existingPatient.TreatmentGoals = patient.TreatmentGoals;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePatient(int id)
        {
            var patient = _iFSPartsMap.Patients.FirstOrDefault(p => p.PatientId == id);

            if (patient == null)
            {
                return NotFound($"Patient with ID {id} not found.");
            }

            _iFSPartsMap.Patients.Remove(patient);
            return NoContent();
        }
    }
}
