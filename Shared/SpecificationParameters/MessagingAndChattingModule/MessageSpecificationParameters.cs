namespace Shared.SpecificationParameters.MessagingAndChattingModule
{
    public class MessageSpecificationParameters
    {
        private const int MaxTake = 50;
        private int take = 20;

        public int Take
        {
            get => take;
            set => take = value > MaxTake ? MaxTake : value;
        }

        public int ConversationId { get; set; }
        public long? BeforeMessageId { get; set; }

    }
}
