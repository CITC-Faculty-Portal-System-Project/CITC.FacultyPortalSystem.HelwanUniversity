namespace Shared.ErrorModels
{
    public class ValidationErrorResponseDetails
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
        public IEnumerable<ValidationErrorDetails> Errors { get; set; } = [];
    }
}
