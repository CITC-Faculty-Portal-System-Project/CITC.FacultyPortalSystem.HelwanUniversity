using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.Enums.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.ProjectsAndCommitteesModule;

namespace Services.Specifications.ProjectsAndCommitteesModule
{
    internal class ParticipationInMagazinesSpecifications : BaseSpecifications<ParticipationInMagazines, int>
    {
        public ParticipationInMagazinesSpecifications(ParticipationInMagazinesSpecificationsParameters parameters, string facultyMemberId)
            : base(pim =>
                  (!pim.IsDeleted &&
                    pim.FacultyMember!.Email == facultyMemberId) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   pim.NameOfMagazine.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   pim.TypeOfParticipation.ValueAr.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   pim.TypeOfParticipation.ValueEn.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase))
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
