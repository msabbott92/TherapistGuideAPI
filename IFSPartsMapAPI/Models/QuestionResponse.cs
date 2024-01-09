namespace IFSPartsMapAPI.Models
{
    public class QuestionResponse
    {
        public int QuestionResponseId { get; set; }
        public string Question { get; set; } = string.Empty;
        public string Response { get; set; } = string.Empty;
        public int IFSPartId { get; set; } 
    }

}
