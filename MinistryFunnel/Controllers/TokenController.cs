using AuthenticationService.Managers;
using AuthenticationService.Models;
using Microsoft.IdentityModel.Tokens;
using MinistryFunnel.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace MinistryFunnel.Controllers
{
    [Route("api/token")]
    public class TokenController : ApiController
    {
        [HttpPost]
        public IHttpActionResult GenerateToken([FromBody] AuthenticationModel authenticationModel)
        {
            if (authenticationModel.api_user == "api_user" && authenticationModel.password == "password")
            {

                //TODO insert email based on username here... will have to get it from AD
                IAuthContainerModel model = GetJWTContainerModel(authenticationModel.username, authenticationModel.email);
                //TODO updaet secret key to somethng complex and move to configuration file 
                IAuthService authService = new JWTService(model.SecretKey);

                string token = authService.GenerateToken(model);

                return Ok(token);
            }     
            else
            {
                return Unauthorized();
            }
            
        }

        [HttpPost]
        public IHttpActionResult ValidateToken(string token)
        {
            IAuthService authService = new JWTService(ConfigurationManager.AppSettings["ApiSecretKey"]);

            if (!authService.IsTokenValid(token))
            {
                return Unauthorized();
            }
            else
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                var z = from claim in jwtToken.Claims
                        where claim.Type == "role"
                        select claim.Value;

                if (z.Contains("discovery_api_edit"))
                {
                    return Ok("access granted");
                }
                        

                return Unauthorized();
            }   
        }


        private static JWTContainerModel GetJWTContainerModel(string name, string email)
        {
            return new JWTContainerModel()
            {
                Claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, "discovery_api_edit"),
                    new Claim(ClaimTypes.Role, "secondary_role")
                }
            };
        }

      
    }
}
