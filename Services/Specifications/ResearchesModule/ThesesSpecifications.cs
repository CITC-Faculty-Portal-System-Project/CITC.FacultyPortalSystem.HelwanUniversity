using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Microsoft.EntityFrameworkCore;
using Shared.Enums.ResearchesModule;
using Shared.SpecificationParameters.ResearchesModule;
using System.Linq.Expressions;

namespace Services.Specifications.ResearchesModule
{
    internal class ThesesSpecifications : BaseSpecifications<Thesis, int>
    {
        public ThesesSpecifications(int id , Guid facultyMemberId) 
                : base(t => t.Id == id && !t.IsDeleted && t.FacultyMemberId == facultyMemberId)
        {
            AddIncludes(t => t.Researches!);
            AddIncludes(t => t.Attachments!);
            AddIncludes(t => t.Grade!);
            AddIncludeWithChain(t => t
                        .Include(t => t.Supervisors!)
                        .ThenInclude(t => t.JobLevel));

        }


        public ThesesSpecifications(ThesesSpecificationParameters parameters , Guid facultyMemberId)
            :base(t => !t.IsDeleted
                    && t.FacultyMemberId == facultyMemberId)
        {


            switch (parameters.Sort)
            {
                case ThesesSortingOptions.TitleASC:
                    AddOrderBy(ts => ts.Title);
                    break;
                case ThesesSortingOptions.TitleDESC:
                    AddOrderByDescending(ts => ts.Title);
                    break;
                case ThesesSortingOptions.EnrollmentDateASC:
                    AddOrderBy(ts => ts.EnrollmentDate);
                    break;
                case ThesesSortingOptions.EnrollmentDateDESC:
                    AddOrderByDescending(ts => ts.EnrollmentDate);
                    break;
                case ThesesSortingOptions.RegisterationDateASC:
                    AddOrderBy(ts => ts.RegistrationDate!);
                    break;
                case ThesesSortingOptions.RegisterationDateDESC:
                    AddOrderByDescending(ts => ts.RegistrationDate!);
                    break;
                default:
                    break;
            }



            AddIncludes(t => t.Researches!);
            AddIncludes(t => t.Attachments!);
            AddIncludes(t => t.Grade!);
            AddIncludeWithChain(t => t
                        .Include(t => t.Supervisors!)
                        .ThenInclude(t => t.JobLevel));

            applyPagination(parameters.PageSize, parameters.PageIndex);

        }
    }
}
