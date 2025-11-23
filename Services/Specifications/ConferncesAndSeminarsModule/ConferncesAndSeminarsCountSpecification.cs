using Domain.Entities.MissionsModule;
using Shared.SpecificationParameters.SemiarsAndConferncesModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications.ConferncesAndSeminarsModule
{
    internal class ConferncesAndSeminarsCountSpecification : BaseSpecifications<ConferencesAndSeminars , int>
    {
        public ConferncesAndSeminarsCountSpecification(SeminarsAndConferncesSpecificationParameters parameters)
                :base(m => m.FacultyMember.Email == parameters.FacultyMemberEmail &&
                    string.IsNullOrEmpty(parameters.SearchCriteria)
                    || m.Name.Contains(parameters.SearchCriteria, StringComparison.CurrentCultureIgnoreCase))
        {

        }


    }
}
