using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.Enums.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Specifications.AcademicDataModule.ProjectsAndCommitteesModule
{
    internal class CommitteesAndAssociationsSpecifications : BaseSpecifications<CommitteesAndAssociations, int>
    {
        public CommitteesAndAssociationsSpecifications(CommitteesAndAssociationsSpecificationsParameters parameters, string facultyMemberId) 
            : base(caa =>
                  !caa.IsDeleted &&
                    caa.FacultyMember!.Email == facultyMemberId &&
                  (parameters.TypeOfCommitteeOrAssociationIds == null || !parameters.TypeOfCommitteeOrAssociationIds.Any() ||
                   parameters.TypeOfCommitteeOrAssociationIds.Contains(caa.TypeOfCommitteeOrAssociationId)) &&
                  (parameters.DegreeOfSubscriptionIds == null || !parameters.DegreeOfSubscriptionIds.Any() ||
                   parameters.DegreeOfSubscriptionIds.Contains(caa.DegreeOfSubscriptionId)) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   caa.NameOfCommitteeOrAssociation.Contains(parameters.Search))
            )
        {
            AddIncludes(caa => caa.TypeOfCommitteeOrAssociation);
            AddIncludes(caa => caa.DegreeOfSubscription);

            switch (parameters.Sort)
            {
                case CommitteesAndAssociationsSortingOptions.DateAsc:
                    AddOrderBy(caa => caa.StartDate);
                    break;
                case CommitteesAndAssociationsSortingOptions.DateDesc:
                    AddOrderByDescending(caa => caa.StartDate);
                    break;
                case CommitteesAndAssociationsSortingOptions.NameAsc:
                    AddOrderBy(caa => caa.NameOfCommitteeOrAssociation);
                    break;
                case CommitteesAndAssociationsSortingOptions.NameDesc:
                    AddOrderByDescending(caa => caa.NameOfCommitteeOrAssociation);
                    break;
                default:
                    break;
            }
            applyPagination(parameters.PageSize, parameters.PageIndex);

        }

        public CommitteesAndAssociationsSpecifications(int id) : base(caa => !caa.IsDeleted && caa.Id == id)
        {

        }
    }
}
