
namespace IFSPartsMapAPI.Models
{
    public class IFSPart
    {
        public int Id { get; set; }
        public string PartName { get; set; } = string.Empty;
        public int PartCategoryId { get; set; } 
        public PartCategory? PartCategory { get; set; } 
        public List<QuestionResponse> QuestionResponses { get; set; } = new List<QuestionResponse>();
        public string? AdditionalNotes { get; set; }
        public int PatientId { get; set; }

        public static implicit operator List<object>(IFSPart v)
        {
            throw new NotImplementedException();
        }
    }

}
