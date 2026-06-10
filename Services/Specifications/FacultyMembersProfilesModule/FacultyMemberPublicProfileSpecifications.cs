using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Services.Specifications.FacultyMembersProfilesModule
{
    public class FacultyMemberPublicProfileSpecifications : BaseSpecifications<FacultyMember, Guid>
    {
        public FacultyMemberPublicProfileSpecifications
            (Guid facultyMemberId) 
            : base(f => !f.IsDeleted && f.Id == facultyMemberId)
        {
            
            
            AddIncludeWithChain(f => f
                .Include(fd => fd.Researcher!)
                .ThenInclude(r => r!.ResearcherInterests!)
                .ThenInclude(ri => ri.Interest));


            AddIncludeWithChain(f => f
             .Include(f => f.ResearchContributions!
                .OrderByDescending(rc => rc.Research!.NoOfCititations)
                .Take(3))
                .ThenInclude(rc => rc.Research));


            AddIncludes(f => f.PersonalData!.Title);
            AddIncludes(f => f.PersonalData!);
            AddIncludes(f => f.PersonalData!.ProfilePicture!);


            AddIncludes(f => f.ScientificMissions
                .OrderByDescending(x => x.Id) 
                .Take(3));



            AddIncludes(f => f.GeneralExperiences
                    .OrderByDescending(ge => ge.Id)
                    .Take(2));
            
            AddIncludes(f => f.TeachingExperiences
                        .OrderByDescending(te => te.Id)
                        .Take(1));
            

        }
    }
}
