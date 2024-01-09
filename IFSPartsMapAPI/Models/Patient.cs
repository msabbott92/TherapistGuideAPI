namespace IFSPartsMapAPI.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Gender { get; set; } = string.Empty;
        public string TreatmentGoals { get; set; } = string.Empty;

        // Relationship with IFSPart
        public List<IFSPart> IFSParts { get; set; } = new List<IFSPart>();
    }
}
