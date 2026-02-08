using Domain.Entities.AcademicDataModule.WritingsAndPatents;
using Shared.SpecificationParameters.AcademicDataModule.WritingsAndPatentsModule;

namespace Services.Specifications.AcademicDataModule.WritingsAndPatentsModule
{
    internal class ScientificWritingsCountSpecifications : BaseSpecifications<ScientificWritings, int>
    {
        public ScientificWritingsCountSpecifications(ScientificWritingsSpecificationParameters parameters, string facultyMemberEmail)
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

        }
    }
}
