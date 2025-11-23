using Domain.Entities.MissionsModule;
using Shared.Enums;
using Shared.SpecificationParameters.SemiarsAndConferncesModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications.ConferncesAndSeminarsModule
{
    internal class ConferncesAndSeminarsSpecification : BaseSpecifications<ConferencesAndSeminars , int>
    {
        public ConferncesAndSeminarsSpecification(SeminarsAndConferncesSpecificationParameters parameters)
                :base(m => m.FacultyMember.Email == parameters.FacultyMemberEmail &&
                    string.IsNullOrEmpty(parameters.SearchCriteria)
                    || m.Name.Contains(parameters.SearchCriteria, StringComparison.CurrentCultureIgnoreCase))
        {
            AddIncludes(a => a.RoleOfParticipation);
            switch (parameters.OrderCriteria)
            {
                case MissionsSortingOptions.ByStartDateASC:
                    AddOrderBy(aq => aq.StartDate);
                    break;
                case MissionsSortingOptions.ByStartDateDESC:
                    AddOrderByDescending(aq => aq.StartDate);
                    break;
                default:
                    break;
            }
            applyPagination(parameters.PageSize, parameters.pageIndex);
        }

        public ConferncesAndSeminarsSpecification(int id) : base(a=>a.Id == id)
        {

        }
    }
}
