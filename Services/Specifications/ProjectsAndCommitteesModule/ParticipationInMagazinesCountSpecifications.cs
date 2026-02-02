using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.ProjectsAndCommitteesModule;

namespace Services.Specifications.ProjectsAndCommitteesModule
{
    internal class ParticipationInMagazinesCountSpecifications : BaseSpecifications<ParticipationInMagazines, int>
    {
        public ParticipationInMagazinesCountSpecifications(ParticipationInMagazinesSpecificationsParameters parameters, string facultyMemberId)
            : base(pim =>
                  (!pim.IsDeleted &&
                    pim.FacultyMember!.Email == facultyMemberId) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   pim.NameOfMagazine.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   pim.TypeOfParticipation.ValueAr.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   pim.TypeOfParticipation.ValueEn.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase))
            )
        {

        }
    }
}
