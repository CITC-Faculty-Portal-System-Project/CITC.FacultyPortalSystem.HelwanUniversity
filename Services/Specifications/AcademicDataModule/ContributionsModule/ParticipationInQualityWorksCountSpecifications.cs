using Domain.Entities.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Services.Specifications.AcademicDataModule.ContributionsModule
{
    internal class ParticipationInQualityWorksCountSpecifications : BaseSpecifications<ParticipationInQualityWorks, int>
    {
        public ParticipationInQualityWorksCountSpecifications(ParticipationInQualityWorksSpecificationParameters parameters, string facultyMemberEmail)
            : base(piqw =>
                  !piqw.IsDeleted &&
                   piqw.FacultyMember!.Email == facultyMemberEmail &&
                   (string.IsNullOrEmpty(parameters.Search) ||
                   piqw.ParticipationTitle.Contains(parameters.Search))
            )
        {
        }

        public ParticipationInQualityWorksCountSpecifications(Guid facultyMemberId)
            : base(piqw =>
                  !piqw.IsDeleted &&
                   piqw.FacultyMemberId == facultyMemberId
            )
        {
        }
    }
}
