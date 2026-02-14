using Domain.Entities.AcademicDataModule.ResearchesModule;
using Domain.Entities.IdentityModule;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Shared.Enums.ResearchesModule;
using Shared.SpecificationParameters.ResearchesModule;
using System.Linq.Expressions;

namespace Services.Specifications.ResearchesModule
{
    internal class ResearchSpecifications : BaseSpecifications<Research, int>
    {
        public ResearchSpecifications(string researchName , Guid facultyMemberId) 
            : base(r => EF.Functions.Like(r.Title, $"%{researchName}%")
            && !r.IsDeleted && r.Contributions!.SingleOrDefault(c => c.ContributorId == facultyMemberId)!
            .IsConfirmed == true && !r.Contributions!
            .SingleOrDefault(r => r.ContributorId == facultyMemberId)!
            .IsDeleted)
        {
            AddIncludes(r => r.Contributions!);
            AddIncludes(r => r.Attachments!);
            AddIncludes(r => r.Cites!);
        }

        public ResearchSpecifications(int id , Guid facultyMemberId)
        : base(r => r.Id == id && !r.IsDeleted &&  
        r.Contributions!.SingleOrDefault(c => c.ContributorId == facultyMemberId)
        !.IsConfirmed == true && !r.Contributions!
            .SingleOrDefault(r => r.ContributorId == facultyMemberId)!
            .IsDeleted)

        {
            AddIncludeWithChain(q => q
                             .Include(r => r.Contributions!
                             .Where(c => c.MemberAcademicName != facultyMemberId.ToString())));
           
            AddIncludes(r => r.Attachments!);
            AddIncludes(r => r.Cites!);
        }


        public ResearchSpecifications(ResearchSpecificationParameters parameters, Guid facultyMemberId)
            : base(r => !r.IsDeleted &&
                    r.Contributions!
                        .SingleOrDefault(c => c.ContributorId == facultyMemberId)!.IsConfirmed == true 
            && !r.Contributions!
            .SingleOrDefault(r => r.ContributorId == facultyMemberId)!
            .IsDeleted &&

            (string.IsNullOrEmpty(parameters.Search) ||
                   r.Title.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   r.JournalOrConfernce.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase) ||
                   r.PubYear.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase)))


        {
            switch (parameters.Sort)
            {
                case ResearchesSortingOptions.TitleASC:
                    AddOrderBy(r => r.Title);
                    break;
                case ResearchesSortingOptions.TitleDESC:
                    AddOrderByDescending(r => r.Title);
                    break;
                case ResearchesSortingOptions.JournalASC:
                    AddOrderBy(r => r.JournalOrConfernce);
                    break;
                case ResearchesSortingOptions.JournalDESC:
                    AddOrderByDescending(r => r.JournalOrConfernce);
                    break;
                case ResearchesSortingOptions.PubYearASC:
                    AddOrderBy(r => Convert.ToInt32(r.PubYear));
                    break;
                case ResearchesSortingOptions.PubYearDESC:
                    AddOrderByDescending(r => Convert.ToInt32(r.PubYear));
                    break;
                default:
                    break;
            }
            applyPagination(parameters.PageSize, parameters.PageIndex);
            AddIncludeWithChain(q => q
                                      .Include(r => r.Contributions!
                                      .Where(c => c.MemberAcademicName != facultyMemberId.ToString())));
            
            AddIncludes(r => r.Attachments!);
            AddIncludes(r => r.Cites!);
        }
    }
}
