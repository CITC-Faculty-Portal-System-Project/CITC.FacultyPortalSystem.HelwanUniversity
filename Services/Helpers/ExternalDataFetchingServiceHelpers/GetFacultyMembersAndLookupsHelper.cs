using Services.Specifications.LookUpItems;

namespace Services.Helpers.ExternalDataFetchingServiceHelpers
{
    public class GetFacultyMembersAndLookupsHelper(IGenericRepository<Lookup, Guid> _lookupRepo,
        IGenericRepository<FacultyMember, Guid> _facultyMemberRepo) 
        : IGetDataFromExternalServiceGetFacultyMembersAndLookupsHelper
    {
        public async Task<Guid> GetFacultyIdByNationalNumberAsync(string nationalNum)
        {
            var spec = new FacultyMemberWithNationalNumberSpecifications(nationalNum);
            return (await _facultyMemberRepo.GetAllAsync(spec)).FirstOrDefault()?.Id ?? Guid.Empty;
        }

        public async Task<Guid> GetLookupIdByNameAsync(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Guid.Empty;

            var spec = new LookUpItemNameSpecification(name);
            return (await _lookupRepo.GetAllAsync(spec)).FirstOrDefault()?.Id ?? Guid.Empty;
        }
    }
}
