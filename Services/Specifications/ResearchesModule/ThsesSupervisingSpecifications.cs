using Domain.Entities.AcademicDataModule.HigherStuidesModule;
using Shared.Enums.ResearchesModule;
using Shared.SpecificationParameters.ResearchesModule;

namespace Services.Specifications.ResearchesModule
{
    internal class ThsesSupervisingSpecifications : BaseSpecifications<Supervising, int>
    {
        public ThsesSupervisingSpecifications(int id)
                : base(ts => !ts.IsDeleted && ts.Id == id)
        {
            AddIncludes(ts => ts.Grade!);
        }


        public ThsesSupervisingSpecifications(ThesesSupervisingSpecificationParameters parameters 
                    , Guid FacultyMemberId)
                : base(ts => !ts.IsDeleted && ts.FacultyMemberId == FacultyMemberId)
        {
            switch (parameters.Sort)
            {
                case ThesesSupervisingSortingOptions.TitleASC:
                    AddOrderBy(ts => ts.Title);
                    break;
                case ThesesSupervisingSortingOptions.TitleDESC:
                    AddOrderByDescending(ts => ts.Title);
                    break;
                case ThesesSupervisingSortingOptions.StudentNameASC:
                    AddOrderBy(ts => ts.StudentName);
                    break;
                case ThesesSupervisingSortingOptions.StudentNameDESC:
                    AddOrderByDescending(ts => ts.StudentName);
                    break;
                case ThesesSupervisingSortingOptions.RegistrationDateASC:
                    AddOrderBy(ts => ts.RegistrationDate!);
                    break;
                case ThesesSupervisingSortingOptions.RegistrationDateDESC:
                    AddOrderByDescending(ts => ts.RegistrationDate!);
                    break;
                case ThesesSupervisingSortingOptions.SupervisionFormationDateASC:
                    AddOrderBy(ts => ts.SupervisionFormationDate!);
                    break;
                case ThesesSupervisingSortingOptions.SupervisionFormationDateDESC:
                    AddOrderByDescending(ts => ts.SupervisionFormationDate!);
                    break;
                case ThesesSupervisingSortingOptions.DiscussionDateASC:
                    AddOrderBy(ts => ts.DiscussionDate!);
                    break;
                case ThesesSupervisingSortingOptions.DiscussionDateDESC:
                    AddOrderByDescending(ts => ts.DiscussionDate!);
                    break;
                case ThesesSupervisingSortingOptions.GrantingDateASC:
                    AddOrderBy(ts => ts.GrantingDate!);
                    break;
                case ThesesSupervisingSortingOptions.GrantingDateDESC:
                    AddOrderByDescending(r => r.GrantingDate!);
                    break;
                default:
                    break;
            }
            applyPagination(parameters.PageSize, parameters.PageIndex);

            AddIncludes(ts => ts.Grade!);
        }
    }
}
