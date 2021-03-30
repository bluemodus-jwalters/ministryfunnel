using AuthenticationService.Managers;
using AuthenticationService.Models;
using Microsoft.IdentityModel.Tokens;
using MinistryFunnel.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
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
            if (authenticationModel.username == "user" && authenticationModel.password == "password")
            {

                //TODO insert email based on username here... will have to get it from AD
                IAuthContainerModel model = GetJWTContainerModel(authenticationModel.username, "jordanwalters@discoverychurch.org");
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
            IAuthContainerModel model = GetJWTContainerModel("Moshe Binieli", "mmoshikoo@gmail.com");
            IAuthService authService = new JWTService(model.SecretKey);
            
            if (!authService.IsTokenValid(token))
            {
                return Unauthorized();
            }
            else
            {
                return Ok("welcome");
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
                    new Claim(ClaimTypes.Role, "allow") //TODO: update this to the role they are really allowed to have
                }
            };
        }
    }
}
