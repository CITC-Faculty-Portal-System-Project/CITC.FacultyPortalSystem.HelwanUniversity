namespace Shared.Dtos.DataFetchingFromExternalService
{
    public record ThesesSupervisorsFetchingDTO
    {
        public string Role { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string JobLevel { get; set; } = string.Empty;
        public string Authority { get; set; } = string.Empty;

    }
}
