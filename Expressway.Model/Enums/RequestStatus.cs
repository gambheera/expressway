using System;
using System.Collections.Generic;
using System.Text;

namespace Expressway.Model.Enums
{
    public enum RequestStatus
    {
        Pending = 1,
        Approved = 2,
        Rejected = 3,
        CancelledByDriver = 4,
        CancelledByPassenger = 5
    }
}
