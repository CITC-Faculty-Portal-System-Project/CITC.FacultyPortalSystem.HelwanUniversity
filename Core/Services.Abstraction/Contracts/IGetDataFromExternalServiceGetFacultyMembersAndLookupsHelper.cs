namespace Services.Abstraction.Contracts
{
    public interface IGetDataFromExternalServiceGetFacultyMembersAndLookupsHelper
    {
        Task<Guid> GetLookupIdByNameAsync(string? name);
        Task<Guid> GetFacultyIdByNationalNumberAsync(string nationalNum);
    }
}
