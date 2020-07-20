using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer
{
    internal class Clients
    {

        public static IEnumerable<Client> Get()
        {
            return new List<Client>
        {
            new Client
            {
                ClientId = "oauthClient",
                ClientName = "ClientApi",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = new List<Secret> {new Secret("clientSecretCode".Sha256())},
                AllowedScopes = new List<string> {"webApi"}
            },

            new Client
            {
                ClientId = "oidcClient_mvc",
                ClientName = "Client_MVC",
                AllowedGrantTypes = GrantTypes.Code,
                ClientSecrets = new List<Secret> {new Secret("clientSecretCode_mvc".Sha256())},
                RedirectUris = new List<string> {"https://localhost:44333/signin-oidc"},
                AllowedScopes = new List<string> {

                    "webApi",
                    "ClientApi",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "role"
                
                }
            }
        };
        }


    }
}
