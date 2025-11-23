using Domain.Entities.MissionsModule;
using Shared.Dtos.MissionsModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications.MissionsModule
{
    internal class MissionCheckingAlreadyExistSpecification : BaseSpecifications<ScientificMissions , int>
    {
        public MissionCheckingAlreadyExistSpecification(MissionAddDto mission): base
            (m => m.MissionName == mission.name && 
            m.StartDate == mission.StartDate && 
            m.EndDate == mission.EndDate &&
            m.UniversityOrFaculty == mission.UniversityOrFaculty
            && m.CountryOrCity == mission.CountryOrCity && 
            m.FacultyMemberId == m.FacultyMemberId)
        {

        }
    }
}
