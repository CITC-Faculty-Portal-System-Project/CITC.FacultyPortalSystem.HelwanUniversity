using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public enum TicketStatus
    {
        Opened = 1,
        InProgress = 2,
        Resolved = 3,
        Reopened = 4,
        Closed = 5,
        WithdrawByUser = 6,
    }
}
