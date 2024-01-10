using Microsoft.AspNetCore.Mvc;
using IFSPartsMapAPI.Models;
using System.Linq;
using IFSPartsMapAPI.Data;

namespace IFSPartsMapAPI.Controllers
{
    [ApiController]
    [Route("api/patients")]
    public class PatientController : ControllerBase
    {
        private readonly IFSPartsMapDbContext _context;

        public PatientController(IFSPartsMapDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public ActionResult<IEnumerable<Patient>> GetPatients()
        {
            return Ok(_context.Patients);
        }

        [HttpGet("{id}")]
        public ActionResult<Patient> GetPatient(int id)
        {
            var patient = _context.Patients.FirstOrDefault(p => p.PatientId == id);

            if (patient == null)
            {
                return NotFound($"Patient with ID {id} not found.");
            }

            return Ok(patient);
        }
        [HttpPost]
        public async Task<ActionResult<Patient>> CreatePatient(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync(); 

            return CreatedAtAction(nameof(GetPatient), new { id = patient.PatientId }, patient);
        }


        [HttpPut("{id}")]
        public IActionResult UpdatePatient(int id, Patient patient)
        {
            var existingPatient = _context.Patients.FirstOrDefault(p => p.PatientId == id);

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
            var patient = _context.Patients.FirstOrDefault(p => p.PatientId == id);

            if (patient == null)
            {
                return NotFound($"Patient with ID {id} not found.");
            }

            _context.Patients.Remove(patient);
            return NoContent();
        }
    }
}
