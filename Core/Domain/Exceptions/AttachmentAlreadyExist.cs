namespace Domain.Exceptions
{
    public class AttachmentAlreadyExist : Exception
    {
        public AttachmentAlreadyExist(string filename)
           : base($"Attachment named {filename} already exists!")
        {
        }
    }
}
