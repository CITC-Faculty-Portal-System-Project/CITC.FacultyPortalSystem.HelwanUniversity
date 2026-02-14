using Domain.Entities.AcademicDataModule.ContributionsModule;
using Shared.Enums.AcademicDataModule.ContributionsModule;
using Shared.SpecificationParameters.AcademicDataModule.ContributionsModule;

namespace Services.Specifications.AcademicDataModule.ContributionsModule
{
    internal class ParticipationInQualityWorksSpecifications : BaseSpecifications<ParticipationInQualityWorks, int>
    {
        public ParticipationInQualityWorksSpecifications(ParticipationInQualityWorksSpecificationParameters parameters, string facultyMemberEmail)
            : base(piqw =>
                  !piqw.IsDeleted &&
                   piqw.FacultyMember!.Email == facultyMemberEmail &&
                   (string.IsNullOrEmpty(parameters.Search) ||
                   piqw.ParticipationTitle.Contains(parameters.Search))
            )
        {
            switch (parameters.Sort)
            {
                case ParticipationInQualityWorksSortingOptions.nameAsc:
                    AddOrderBy(piqw => piqw.ParticipationTitle);
                    break;
                case ParticipationInQualityWorksSortingOptions.nameDesc:
                    AddOrderByDescending(piqw => piqw.ParticipationTitle);
                    break;
                case ParticipationInQualityWorksSortingOptions.startDateAsc:
                    AddOrderBy(piqw => piqw.StartDate);
                    break;
                case ParticipationInQualityWorksSortingOptions.startDateDesc:
                    AddOrderByDescending(piqw => piqw.StartDate);
                    break;
                default:
                    break;
            }
            applyPagination(parameters.PageSize, parameters.PageIndex);
        }
        public ParticipationInQualityWorksSpecifications(int id) : base(piqw => !piqw.IsDeleted && piqw.Id == id)
        {
        }
    }
}