using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationPolicies
{
    public static class AuthenticationConfiguration
    {
        public const string AuthenticationScheme = "Cookie";
    }

    public static class AuthorizationPolicies
    {
        public const string EUCustoms = "EUCustomsLane";
        public const string NothingToDeclare = "NothingToDeclare";
        public const string GoodsToDeclare = "GoodsToDeclare";
    }
}
