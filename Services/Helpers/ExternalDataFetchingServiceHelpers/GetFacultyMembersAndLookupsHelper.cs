using Domain.Contracts;
using Domain.Entities;
using Services.Specifications.LookUpItems;

namespace Services.Helpers.ExternalDataFetchingServiceHelpers
{
    public sealed class GetFacultyMembersAndLookupsHelper(IUnitOfWork _unitOfWork)
        : IGetDataFromExternalServiceGetFacultyMembersAndLookupsHelper
    {
        public async Task<Guid> GetFacultyIdByNationalNumberAsync(string nationalNum)
        {
            var repo = _unitOfWork.GetRepository<FacultyMember, Guid>();

            var spec = new FacultyMemberWithNationalNumberSpecifications(nationalNum);

            return (await repo.GetAllAsync(spec)).FirstOrDefault()?.Id ?? Guid.Empty;
        }

        public async Task<Guid> GetLookupIdByNameAsync(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return Guid.Empty;

            var repo = _unitOfWork.GetRepository<Lookup, Guid>();

            var spec = new LookUpItemNameSpecification(name);

            return (await repo.GetAllAsync(spec)).FirstOrDefault()?.Id ?? Guid.Empty;
        }
    }
}