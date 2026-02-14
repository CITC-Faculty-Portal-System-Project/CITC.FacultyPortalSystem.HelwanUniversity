using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.Enums.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Specifications.AcademicDataModule.ProjectsAndCommitteesModule
{
    internal class ParticipationInMagazinesSpecifications : BaseSpecifications<ParticipationInMagazines, int>
    {
        public ParticipationInMagazinesSpecifications(ParticipationInMagazinesSpecificationsParameters parameters, string facultyMemberId)
            : base(pim =>
                  !pim.IsDeleted &&
                    pim.FacultyMember!.Email == facultyMemberId &&
                  (parameters.TypeOfParticipationIds == null || !parameters.TypeOfParticipationIds.Any() ||
                   parameters.TypeOfParticipationIds.Contains(pim.TypeOfParticipationId)) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   pim.NameOfMagazine.Contains(parameters.Search))
            )
        {
            AddIncludes(P => P.TypeOfParticipation);
            switch (parameters.Sort)
            {
                case ParticipationInMagazinesSortingOptions.NameAsc:
                    AddOrderBy(pim => pim.NameOfMagazine);
                    break;
                case ParticipationInMagazinesSortingOptions.NameDesc:
                    AddOrderByDescending(pim => pim.NameOfMagazine);
                    break;
                default:
                    break;
            }
            applyPagination(parameters.PageSize, parameters.PageIndex);

        }

        public ParticipationInMagazinesSpecifications(int id) : base(pim => !pim.IsDeleted && pim.Id == id)
        {
            AddIncludes(P => P.TypeOfParticipation);
        }
    }
}
