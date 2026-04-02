
using Domain.Entities.EntitesAttachments;
using Services.Abstraction.Enums;

namespace Services.Implementations.AttachmentsModule.Helpers.Handlers
{
    public sealed class AcademicQualificationAttachmentHandler
        : AttachmentContextHandlerBase<AcademicQualificationAttachment>
    {
        public AcademicQualificationAttachmentHandler(AttachmentCore svc) : base(svc) { }

        public override AttachmentContext Context => AttachmentContext.AcademicQualification;

        protected override void SetOwner(AcademicQualificationAttachment a, int ownerId)
            => a.QualificationId = ownerId;

        protected override bool MatchOwner(AcademicQualificationAttachment a, int ownerId)
            => a.QualificationId == ownerId;

        protected override int GetOwnerId(AcademicQualificationAttachment a)
            => a.QualificationId;
    }
}
