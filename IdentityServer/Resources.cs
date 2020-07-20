using IdentityServer4.Models;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer
{
    internal class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
             
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResources.Email(),
            new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> {"role"}
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
            new ApiResource
                {
                    Name = "webApi",
                    DisplayName = "API #1",
                    Description = "Allow the application to access API #1 on your behalf",
                    Scopes = new List<string> {"webApi"},
                    ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
                    UserClaims = new List<string> {"role"}
                },

            new ApiResource
                {
                    Name = "ClientApi",
                    DisplayName = "API #2",
                    Description = "Allow the application to access API #1&2 on your behalf",
                    Scopes = new List<string> {"webApi", "ClientApi"},
                    ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
                    UserClaims = new List<string> {"role"}
                },

            //new ApiResource
            //    {
            //        Name = "Client_MVC",
            //        DisplayName = "API MVC",
            //        Description = "Allow the application to access API MVC on your behalf",
            //        Scopes = new List<string> {"webApi", "ClientApi"},
            //        ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
            //        UserClaims = new List<string> {"role"}
            //    }

            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new[]
            {
                new ApiScope("webApi", "Read Access to API #1"),
                new ApiScope("ClientApi", "Access to API #2"),
                //new ApiScope("oidc", "OpenIDConnect"),
                //new ApiScope("Client_MVC", "Access to MVC_API")
            };
        }
    }
}
