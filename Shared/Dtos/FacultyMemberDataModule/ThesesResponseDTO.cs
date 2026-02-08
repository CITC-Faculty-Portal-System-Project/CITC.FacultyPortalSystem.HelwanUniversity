using Shared.Dtos;

namespace Shared.Enums.ResearchesModule
{
    public record ThesesResponseDTO
    {
        public int Id { get; set; }
        public ThesisType Type { get; set; }
        public string? Link { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public LookupItemDto Grade { get; set; }
        public DateOnly EnrollmentDate { get; set; }
        public DateOnly RegistrationDate { get; set; }
        public DateOnly? InternalGradeDate { get; set; }
        public DateOnly? SupervisionConfirmationDate { get; set; }

        //public List<Reseat MyProperty { get; set; }


    }
}
