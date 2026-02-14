
namespace Services.Implementations.AttachmentsModule
{
    internal class OwnerBindingMap
    {
        public required Func<BaseAttachmentEntity> Create { get; init; }
        public required Action<BaseAttachmentEntity, int> SetOwner { get; init; }
        public required Func<BaseAttachmentEntity, int, bool> MatchOwner { get; init; }
    }
}
