using Services.Abstraction.Contracts.FacultyMembersPublicProfileModule;
using Services.Global;
using Services.Specifications.FacultyMembersProfilesModule;
using Shared.Dtos.AcademicDataModule.ExperiencesModule;
using Shared.Dtos.FacultyMembersProfilesModule;
using Shared.SpecificationParameters.FacultyMembersProfilesModule;
using Shared.SpecificationParameters.ReportsAndDashboard.PDF.ResearchesModule;

namespace Services.Implementations.FacultyMembersPublicProfileModule
{
    public class FacultyMemberPublicProfileService(
      IUnitOfWork unitOfWork,
      IAuthenticationService authenticationService,
      IMapper mapper) : BaseService<FacultyMember, Guid>(unitOfWork, authenticationService, mapper)
        , IFacultyMemberPublicProfileService
    {
        protected override string EntityName => "Faculty Member";

        public async Task<PaginatedResult<OtherUsersPageResponseDTO>> GetAllFacultyMembersProfiles(FacultyMembersProfileSpecificationParamters paramters)
        {
            var members = await Repo.GetAllAsync(new OtherUsersPageSpecifications(paramters , paramters.PageIndex , paramters.PageSize, true));

            var count = await Repo.CountAsync(new  OtherUsersPageSpecifications(paramters));

            var mapped = Mapper.Map <IEnumerable<OtherUsersPageResponseDTO>>(members);
            
            return new PaginatedResult<OtherUsersPageResponseDTO>(
                paramters.PageIndex,
                mapped.Count(),
                count,
                mapped);
        }

        public async Task<FacultyMemberPublicProfileResponseDTO> GetFacultyMemberPublicProfile(Guid facultyMemberId)
        {
            var member = await Repo.GetAsync(new FacultyMemberPublicProfileSpecifications(facultyMemberId))
                ?? throw NotFound();


            return Mapper.Map<FacultyMemberPublicProfileResponseDTO>(member);
            
        }

        public async Task<IEnumerable<OtherUsersPageResponseDTO>> SearchMemberPublicProfile(BaseFacultyMemberProfileSpecificationParamters paramters)
        {
            var members = await Repo.GetAllAsync(new OtherUsersPageSpecifications(paramters));

            return  Mapper.Map<IEnumerable<OtherUsersPageResponseDTO>>(members);

        }
    }
}
