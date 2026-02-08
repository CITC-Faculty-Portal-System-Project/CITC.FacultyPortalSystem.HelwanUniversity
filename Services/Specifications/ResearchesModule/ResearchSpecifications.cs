using Domain.Entities.AcademicDataModule.ResearchesModule;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Services.Specifications.ResearchesModule
{
    internal class ResearchSpecifications : BaseSpecifications<Research, int>
    {
        public ResearchSpecifications(string researchName , Guid facultyMemberId) 
            : base(r => EF.Functions.Like(r.Title, $"%{researchName}%")
            && !r.IsDeleted && r.Contributions!.Any(c => c.ContributorId == facultyMemberId))
        {
            AddIncludes(r => r.Contributions!);
            AddIncludes(r => r.Attachments!);
            AddIncludes(r => r.Cites!);
        }

        public ResearchSpecifications(int id , Guid facultyMemberId)
        : base(r => r.Id == id && !r.IsDeleted && 
        r.Contributions!.Any(c => c.ContributorId == facultyMemberId))

        {
            AddIncludes(r => r.Contributions!);
            AddIncludes(r => r.Attachments!);
            AddIncludes(r => r.Cites!);
        }
    }
}
