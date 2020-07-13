using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApiAuth.AuthRequirements
{
    public class JwtRequirements : IAuthorizationRequirement { }

    public class JwtRequirementsHandler : AuthorizationHandler<JwtRequirements>
    {
        private readonly HttpClient _client;
        private readonly HttpContext _httpcontext;

        public JwtRequirementsHandler(IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor)
        {
            _client = httpClientFactory.CreateClient();
            _httpcontext = httpContextAccessor.HttpContext;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            JwtRequirements requirement)
        {
            if(_httpcontext.Request.Query.TryGetValue("Authorization", out var authHeader))
            {

                var accessToken = authHeader.ToString().Split(' ')[1];

                var response = await _client
                    .GetAsync($"https://localhost:44310/auth/validate?access_token={accessToken}");

                if(response.StatusCode == HttpStatusCode.OK)
                {
                    context.Succeed(requirement);
                }
            }

        }
    }
}
