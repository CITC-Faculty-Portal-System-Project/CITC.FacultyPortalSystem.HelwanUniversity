using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Specifications.AcademicDataModule.ProjectsAndCommitteesModule
{
    internal class ParticipationInMagazinesCountSpecifications : BaseSpecifications<ParticipationInMagazines, int>
    {
        public ParticipationInMagazinesCountSpecifications(ParticipationInMagazinesSpecificationsParameters parameters, string facultyMemberId)
            : base(pim =>
                  !pim.IsDeleted &&
                    pim.FacultyMember!.Email == facultyMemberId &&
                  (parameters.TypeOfParticipationIds == null || !parameters.TypeOfParticipationIds.Any() ||
                   parameters.TypeOfParticipationIds.Contains(pim.TypeOfParticipationId)) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   pim.NameOfMagazine.Contains(parameters.Search))
            )
        {

        }
    }
}
