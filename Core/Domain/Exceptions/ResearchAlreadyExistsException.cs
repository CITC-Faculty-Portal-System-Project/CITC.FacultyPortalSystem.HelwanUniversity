
namespace Domain.Exceptions
{
    public class ResearchAlreadyExistsException : Exception
    {
        public ResearchAlreadyExistsException(string DOI)
          : base($"Research with DOI {DOI} already exists!")
        {
        }
    }
}
