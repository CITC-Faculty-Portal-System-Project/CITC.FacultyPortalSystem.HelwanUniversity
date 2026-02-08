using Domain.Entities.AcademicDataModule.ResearchesModule;
using Microsoft.EntityFrameworkCore;

namespace Services.Specifications.ResearchesModule
{
    internal class ResearcherProfileSpceification : BaseSpecifications<ResearcherProfile , int>
    {
        public ResearcherProfileSpceification(Guid facultyMemberId):
            base(rp => rp.FacultyMemberId == facultyMemberId && !rp.IsDeleted)
        {
            AddIncludes(rp => rp.ResearcherCites!);
            AddIncludeWithChain(rp => rp.Include(rp =>rp.ResearcherInterests!)
                                .ThenInclude(rp => rp.Interest));
        }
    }
}
