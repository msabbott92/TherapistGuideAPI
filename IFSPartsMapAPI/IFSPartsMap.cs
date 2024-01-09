using IFSPartsMapAPI.Models;
using System.Collections.Generic;

namespace IFSPartsMapAPI
{
    public class IFSPartsMap
    {
        public List<IFSPart> Parts { get; private set; }
        private List<PartCategory> Categories { get; set; }

        public List<Patient> Patients { get; private set; }

        public static IFSPartsMap Current { get; } = new IFSPartsMap();

        public IFSPartsMap()
        {

            Categories = new List<PartCategory>
            {
                new PartCategory { PartCategoryId = 0, Name = "Manager" },
                new PartCategory { PartCategoryId = 1, Name = "Exile" },
                new PartCategory { PartCategoryId = 2, Name = "Firefighter" },
                new PartCategory { PartCategoryId = 3, Name = "Other" }
            };

            Parts = new List<IFSPart>
            {
                new IFSPart()
                {
                    PatientId = 0,
                    Id = 0,
                    PartName = "The Controller",
                    PartCategory = Categories[0],
                    QuestionResponses = new List<QuestionResponse> { 
                        new QuestionResponse() 
                        {
                            QuestionResponseId = 1,
                            Question = "How old is this part?",
                            Response = "It's between 8 and 10 years old",
                            IFSPartId = 0,
                        } }
                },
                new IFSPart()
                {
                    PatientId = 0,
                    Id = 1,
                    PartName = "The Peacemaker",
                    PartCategory = Categories[1],
                    QuestionResponses = new List<QuestionResponse> {
                        new QuestionResponse()
                        {
                            QuestionResponseId = 1,
                            Question = "How old is this part?",
                            Response = "About 14 years old",
                            IFSPartId = 1,
                        } }
                },
                new IFSPart()
                {
                    PatientId = 1,
                    Id = 2,
                    PartName = "The Entertainer",
                    PartCategory = Categories[0],
                    QuestionResponses = new List<QuestionResponse> {
                        new QuestionResponse()
                        {
                            QuestionResponseId = 1,
                            Question = "How old is this part?",
                            Response = "10 years old",
                            IFSPartId = 2,
                        } }
                },
            };
            Patients = new List<Patient>
            {
                new Patient()
                {
                    PatientId = 0,
                    FirstName = "Matthew",
                    LastName = "Abbott",
                    Age = 31,
                    Gender = "Male",
                    TreatmentGoals = "Working through past developmental trauma.",
                    IFSParts = Parts != null ? Parts.Where(p => p.PatientId == 0).ToList() : new List<IFSPart>()


                },
                new Patient()
                {
                    PatientId= 1,
                    FirstName = "Josh",
                    LastName = "Crouch",
                    Age = 33,
                    Gender = "Male",
                    TreatmentGoals = "Cope with relationship anxiety.",
                    IFSParts = Parts != null ? Parts.Where(p => p.PatientId == 1).ToList() : new List<IFSPart>()
                }
            };
        }
    }
}
