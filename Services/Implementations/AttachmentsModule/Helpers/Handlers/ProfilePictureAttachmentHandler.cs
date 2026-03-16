using Domain.Entities.EntitesAttachments;
using Services.Abstraction.Enums;

namespace Services.Implementations.AttachmentsModule.Helpers.Handlers
{
    public sealed class ProfilePictureAttachmentHandler
         : AttachmentContextHandlerBase<ProfilePictures>
    {
        public ProfilePictureAttachmentHandler(AttachmentCore svc) : base(svc) { }

        public override AttachmentContext Context => AttachmentContext.ProfilePicture;

        protected override void SetOwner(ProfilePictures a, int ownerId)
            => a.PersonalDataId = ownerId;

        protected override bool MatchOwner(ProfilePictures a, int ownerId)
            => a.PersonalDataId == ownerId;

        protected override int GetOwnerId(ProfilePictures a)
            => a.PersonalDataId;
    }
}
