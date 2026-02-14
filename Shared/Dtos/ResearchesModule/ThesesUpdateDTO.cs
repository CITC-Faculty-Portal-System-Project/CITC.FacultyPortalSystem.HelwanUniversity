using Microsoft.AspNetCore.Http;
using Shared.Common;
using Shared.Dtos.AttachmentsModule;
using Shared.Enums.ResearchesModule;

namespace Shared.Dtos.ResearchesModule
{
    public record ThesesUpdateDTO
    {
        public ThesisType Type { get; set; }
        public string? Link { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public Guid GradeId { get; set; }
        public DateOnly EnrollmentDate { get; set; }
        public DateOnly RegistrationDate { get; set; }
        public DateOnly? InternalGradeDate { get; set; }
        public DateOnly? SupervisionConfirmationDate { get; set; }
        public List<ThesesSupervisorResponseDTO>? SupervisorsToDelete { get; set; }
        public List<ResearchResponseDTO>? ResearchesToDelete { get; set; }
        public List<AttachmentResponseDTO>? AttachmentsToDelete { get; set; }
        public IEnumerable<Patch<int, ThesesSupervisorDTO>>? SupervisorsToUpdate { get; set; }
        public IEnumerable<Patch<int, ResearchDTO>>? ResearchesToUpdate { get; set; }
        public List<ThesesSupervisorDTO>? SupervisorsToAdd { get; set; }
        public List<ResearchDTO>? ResearchesToAdd { get; set; }
        public List<IFormFile>? AttachmentsToAdd { get; set; }

    }
}
