using Domain.Entities.AcademicDataModule.WritingsAndPatents;
using Shared.Enums.AcademicDataModule.WritingsAndPatentsModule;
using Shared.SpecificationParameters.AcademicDataModule.WritingsAndPatentsModule;

namespace Services.Specifications.AcademicDataModule.WritingsAndPatentsModule
{
    internal class ScientificWritingsSpecifications : BaseSpecifications<ScientificWritings, int>
    {
        public ScientificWritingsSpecifications(ScientificWritingsSpecificationParameters parameters, string facultyMemberEmail) 
            : base(sw =>
                  !sw.IsDeleted &&
                    sw.FacultyMember!.Email == facultyMemberEmail &&
                  (parameters.AuthorRoleIds == null || !parameters.AuthorRoleIds.Any() ||
                   parameters.AuthorRoleIds.Contains(sw.AuthorRoleId)) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   sw.Title.Contains(parameters.Search) ||
                   sw.PublishingHouse.Contains(parameters.Search))
            )
        {
            AddIncludes(sw => sw.AuthorRole);
            switch (parameters.Sort)
            {
                case ScientificWritingsSortingOptions.DateAsc:
                    AddOrderBy(sw => sw.PublishingDate);
                    break;
                case ScientificWritingsSortingOptions.DateDesc:
                    AddOrderByDescending(sw => sw.PublishingDate);
                    break;
                case ScientificWritingsSortingOptions.nameAsc:
                    AddOrderBy(sw => sw.Title);
                    break;
                case ScientificWritingsSortingOptions.nameDesc:
                    AddOrderByDescending(sw => sw.Title);
                    break;
                default:
                    break;
            }
            applyPagination(parameters.PageSize, parameters.PageIndex);
        }

        public ScientificWritingsSpecifications(int id) : base(sw => !sw.IsDeleted && sw.Id == id)
        {
            AddIncludes(sw => sw.AuthorRole);
        }
    }
}
