using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationPolicies
{
    public interface IPassportInformationRepository
    {
        bool IsPartOfEU(string country);
    }
}
