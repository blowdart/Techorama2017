using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationPolicies
{
    public enum CustomsLane
    {
        EU,
        NothingToDeclare,
        GoodsToDeclare
    }

    public class CustomsLaneRequirement : IAuthorizationRequirement
    {
        public CustomsLane Lane { get; set; }
    }
}
