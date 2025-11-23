using Domain.Entities.MissionsModule;
using Shared.SpceificationParameters.MissionsModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications.MissionsModule
{
    internal class MissionsCountSpecification : BaseSpecifications<ScientificMissions , int>
    {
        public MissionsCountSpecification(MissionSpecificationParamaters specificationParamaters)
                : base(m => m.FacultyMember.Email == specificationParamaters.FacultyMemberEmail &&
                    string.IsNullOrEmpty(specificationParamaters.SearchCriteria)
                    || m.MissionName.Contains(specificationParamaters.SearchCriteria, StringComparison.CurrentCultureIgnoreCase)
                )
        {

        }
    }
}
