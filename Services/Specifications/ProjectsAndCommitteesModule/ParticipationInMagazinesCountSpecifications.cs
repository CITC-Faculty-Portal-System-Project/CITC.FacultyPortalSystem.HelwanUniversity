using Domain.Entities.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.ProjectsAndCommitteesModule;

namespace Services.Specifications.ProjectsAndCommitteesModule
{
    internal class ParticipationInMagazinesCountSpecifications : BaseSpecifications<ParticipationInMagazines, int>
    {
        public ParticipationInMagazinesCountSpecifications(ParticipationInMagazinesSpecificationsParameters parameters)
            : base(pim =>
                  (!pim.IsDeleted &&
                    pim.FacultyMember!.Email == parameters.FacultyMemberEmail) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   pim.NameOfMagazine.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   pim.TypeOfParticipation.ValueAr.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   pim.TypeOfParticipation.ValueEn.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase))
            )
        {

        }
    }
}
