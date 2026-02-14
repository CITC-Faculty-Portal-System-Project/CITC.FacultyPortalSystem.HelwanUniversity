using Domain.Entities.AcademicDataModule.ProjectsAndCommitteesModule;
using Shared.SpecificationParameters.AcademicDataModule.ProjectsAndCommitteesModule;

namespace Services.Specifications.AcademicDataModule.ProjectsAndCommitteesModule
{
    internal class CommitteesAndAssociationsCountSpecifications : BaseSpecifications<CommitteesAndAssociations, int>
    {
        public CommitteesAndAssociationsCountSpecifications(CommitteesAndAssociationsSpecificationsParameters parameters, string facultyMemberId)
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

        }
    }
}
