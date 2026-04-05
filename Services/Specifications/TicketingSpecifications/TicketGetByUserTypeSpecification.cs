using Domain.Entities.AdminModule;
using Shared.Enums.TicketingModule;
using Shared.SpecificationParameters.TicketingModule;
using System.Linq.Expressions;

namespace Services.Specifications.TicketingSpecifications
{
    internal class TicketGetByUserTypeSpecification : BaseSpecifications<Ticket, int>
    {
        public TicketGetByUserTypeSpecification
            (TicketSepcificationParameters parameters,
            Guid userId,
            TicketViewScope scope) : base(t =>
                !t.IsDeleted &&
                (
                    scope == TicketViewScope.Sender
                        ? t.SenderId == userId
                        : t.AssignedToId == userId
                ) &&
                (
                    string.IsNullOrEmpty(parameters.Search) ||
                    t.Title.Contains(parameters.Search) ||
                    t.Description.Contains(parameters.Search)
                ) &&
                (
                    !parameters.Type.HasValue ||
                    t.Type == (Domain.Enums.TicketType)parameters.Type.Value
                ) &&
                (
                    !parameters.Status.HasValue ||
                    t.Status == (Domain.Enums.TicketStatus)parameters.Status.Value
                ) &&
                (
                    !parameters.Priority.HasValue ||
                    t.Priority == (Domain.Enums.TicketPriority)parameters.Priority.Value
                )
            )
        {
            switch (parameters.Sort)
            {
                case TicketsSortingOptions.TitleASC:
                    AddOrderBy(p => p.Title);
                    break;

                case TicketsSortingOptions.TitleDESC:
                    AddOrderByDescending(p => p.Title);
                    break;

                case TicketsSortingOptions.DescriptionASC:
                    AddOrderBy(p => p.Description);
                    break;

                case TicketsSortingOptions.DescriptionDESC:
                    AddOrderByDescending(p => p.Description);
                    break;

                case TicketsSortingOptions.CreatedAtASC:
                    AddOrderBy(p => p.CreatedAt);
                    break;

                case TicketsSortingOptions.CreatedAtDESC:
                    AddOrderByDescending(p => p.CreatedAt);
                    break;

                default:
                    AddOrderByDescending(p => p.CreatedAt);
                    break;
            }

            applyPagination(parameters.PageSize, parameters.PageIndex);
        }
    }
    }

