using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthorizationPolicies
{
    public class PassportInformationRepository : IPassportInformationRepository
    {
        List<string> _EUCountries = new List<string>
        {
            "GBR",
            "NLD"
        };

        public bool IsPartOfEU(string country)
        {
            if (_EUCountries.Contains(country))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
