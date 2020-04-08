using System;
using System.Collections.Generic;
using System.Text;

namespace ticketSupport
{
    public enum TicketSupportStatus
    {
        Open,
        Active,
        Pending,
        Closed
    }

    public enum TicketSupportLevel
    {
        Level1 = 1, 
        Level2, 
        Level3
    }
}
