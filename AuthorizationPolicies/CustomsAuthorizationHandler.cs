using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuthorizationPolicies
{
    public class CustomsAuthorizationHandler : AuthorizationHandler<CustomsLaneRequirement>
    {
        IPassportInformationRepository _repository;

        public CustomsAuthorizationHandler(IPassportInformationRepository repository)
        {
            _repository = repository;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            CustomsLaneRequirement requirement)
        {
            var country =
                context.User.FindFirst(
                    c => c.Type == ClaimTypes.Country &&
                    c.Issuer == "urn:CDG.PassportControl").Value;

            switch (requirement.Lane)
            {
                case CustomsLane.EU:
                    if (_repository.IsPartOfEU(country))
                    {
                        context.Succeed(requirement);
                    }
                    break;

                case CustomsLane.GoodsToDeclare:
                    context.Succeed(requirement);
                    break;

                case CustomsLane.NothingToDeclare:
                    context.Succeed(requirement);
                    break;

            }

            return Task.CompletedTask;
        }
    }
}
