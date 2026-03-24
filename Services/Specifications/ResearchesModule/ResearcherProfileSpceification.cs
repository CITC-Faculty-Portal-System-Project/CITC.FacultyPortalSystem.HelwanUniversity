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

            AddIncludeWithChain(rp => rp.Include(rp => rp.CoAuthors!)
                                        .ThenInclude(co => co.CoAuthor));
        }

        public ResearcherProfileSpceification
            (string scholarProfileLink)
            : base(rp => string.Equals(rp.ScholarProfileLink, scholarProfileLink)
                && !rp.IsDeleted)
        {
            AddIncludes(rp => rp.ResearcherCites!);
        }

    }
}
