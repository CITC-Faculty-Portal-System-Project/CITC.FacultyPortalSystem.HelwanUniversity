using Domain.Entities.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.ProjectsAndCommitteesModule;

namespace Services.Specifications.ProjectsAndCommitteesModule
{
    internal class CommitteesAndAssociationsCountSpecifications : BaseSpecifications<CommitteesAndAssociations, int>
    {
        public CommitteesAndAssociationsCountSpecifications(CommitteesAndAssociationsSpecificationsParameters parameters)
            : base(caa =>
                  (!caa.IsDeleted &&
                    caa.FacultyMember!.Email == parameters.FacultyMemberEmail) &&
                  (string.IsNullOrEmpty(parameters.Search) ||
                   caa.NameOfCommitteeOrAssociation.Contains(parameters.Search, StringComparison.CurrentCultureIgnoreCase))
            )
        {

        }
    }
}
