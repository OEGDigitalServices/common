using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orange.Common.Entities
{
    public enum DialStatus
    {
        Active,
        SoftDisconnected,
        VoluntarySuspended,
        InvoluntarySuspended,
        Inactive,
        PermentlyDeactive,
        Deactive,
        Suspended,
        Hold
    }
}