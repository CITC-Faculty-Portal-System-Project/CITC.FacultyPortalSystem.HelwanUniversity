using Domain.Models;

namespace Domain.Exceptions
{
    public sealed class ValidationException : Exception
    {
        public IEnumerable<ValidationError> Errors { get; }

        public ValidationException(IEnumerable<ValidationError> errors)
            : base("One or more validation failures have occurred.")
        {
            Errors = errors;
        }
    }
}
