namespace Shared.ErrorModels
{
    public class ValidationErrorDetails
    {
        public string Field { get; set; } = string.Empty;
        public IEnumerable<string> Errors { get; set; } = [];
    }
}
