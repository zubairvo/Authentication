using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace AuthServer.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Authorize(
            string response_type, //Authorization flow type
            string client_id, // Client Id
            string redirect_uri, // redirect
            string scope, // what info do i want e.g Email, Telephone and ClientCookie
            string state) // Random Generated string to confirm client
        {

            var query = new QueryBuilder();

            query.Add("redirectUri", redirect_uri);
            query.Add("state", state);

            return View(model: query.ToString());
        }

        [HttpPost]
        public IActionResult Authorize(
            string username, 
            string redirect_uri,
            string state)
        {

            const string code = "uricode";

            var query = new QueryBuilder();
            query.Add("code", code);
            query.Add("state", state);

            return Redirect($"{redirect_uri}{query.ToString()}");
        }

        public async Task<IActionResult> Token(
            
            string grant_type,
            string code,
            string redirect_uri,
            string client_id)
        {
            //validating code
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "userid"),
                new Claim("CustomCLaim" , "CustomCookie")

            };

            var secretBytes = Encoding.UTF8.GetBytes(Constants.Secret);
            var key = new SymmetricSecurityKey(secretBytes);
            var algorithm = SecurityAlgorithms.HmacSha256;

            var signingCredentials = new SigningCredentials(key, algorithm);

            var token = new JwtSecurityToken(

                Constants.Issuer,
                Constants.Audience,
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(1),
                signingCredentials);

            var access_token = new JwtSecurityTokenHandler().WriteToken(token);

            var responseObject = new
            {
                access_token,
                token_type = "Bearer",
                raw_claim = "OAuthTut"
            };

            var responseJson = JsonConvert.SerializeObject(responseObject);
            var response_bytes = Encoding.UTF8.GetBytes(responseJson);

            await Response.Body.WriteAsync(response_bytes, 0, response_bytes.Length);

            return Redirect(redirect_uri);
        }
    }
}
