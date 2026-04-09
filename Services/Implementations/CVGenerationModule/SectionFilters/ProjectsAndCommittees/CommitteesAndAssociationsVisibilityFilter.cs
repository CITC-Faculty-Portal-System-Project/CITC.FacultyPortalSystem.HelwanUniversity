using Services.Abstraction.Contracts.CVGenerationModule;
using Shared.Dtos.CVGenerationModule;
using Shared.Models.CVGenerationModule;
using static Services.Implementations.CVGenerationModule.VisibilityFilterHelper;

namespace Services.Implementations.CVGenerationModule.SectionFilters.ProjectsAndCommittees
{
    public class CommitteesAndAssociationsVisibilityFilter : ICVSectionVisibilityFilter
    {
        public void Apply(CVResponseDTO response, CVVisibilityConfig config, bool isPublic = false)
        {
            var settings = config.CommitteesAndAssociations;

            if (!settings.ShowCommitteesAndAssociations && isPublic == false)
            {
                response.CommitteesAndAssociations.Clear();
                return;
            }

            if (!settings.ShowNameOfCommitteeOrAssociation && !settings.ShowTypeOfCommitteeOrAssociation && !settings.ShowDegreeOfSubscription && !settings.ShowCommitteesAndAssociationsStartDate && !settings.ShowCommitteesAndAssociationsEndDate && isPublic == false)
            {
                response.CommitteesAndAssociations.Clear();
                return;
            }

            foreach(var ca in response.CommitteesAndAssociations ?? [])
            {
                
                
                if(isPublic == true)
                {
                    HideIfFalse(settings.ShowNameOfCommitteeOrAssociationForPublic, () => ca.NameOfCommitteeOrAssociation = null!);
                    HideIfFalse(settings.ShowTypeOfCommitteeOrAssociationForPublic, () => ca.TypeOfCommitteeOrAssociation = null!);
                    HideIfFalse(settings.ShowDegreeOfSubscriptionForPublic, () => ca.DegreeOfSubscription = null!);
                    HideIfFalse(settings.ShowCommitteesAndAssociationsStartDateForPublic, () => ca.StartDate = null);
                    HideIfFalse(settings.ShowCommitteesAndAssociationsEndDateForPublic, () => ca.EndDate = null);
                }

                HideIfFalse(settings.ShowNameOfCommitteeOrAssociation, () => ca.NameOfCommitteeOrAssociation = null!);
                HideIfFalse(settings.ShowTypeOfCommitteeOrAssociation, () => ca.TypeOfCommitteeOrAssociation = null!);
                HideIfFalse(settings.ShowDegreeOfSubscription, () => ca.DegreeOfSubscription = null!);
                HideIfFalse(settings.ShowCommitteesAndAssociationsStartDate, () => ca.StartDate = null);
                HideIfFalse(settings.ShowCommitteesAndAssociationsEndDate, () => ca.EndDate = null);
            }
        }
    }
}
