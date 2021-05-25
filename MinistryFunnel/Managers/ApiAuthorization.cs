using AuthenticationService.Managers;
using AuthenticationService.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace MinistryFunnel.Managers
{
    public class ApiAuthorization : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //If needed, we can get the roles from the actionContext that is passed in via the attribute roles

            IAuthContainerModel model = GetJWTContainerModel("Moshe Binieli", "mmoshikoo@gmail.com");
            IAuthService authService = new JWTService(model.SecretKey);

            var token = actionContext.Request.Headers.Authorization.ToString();

            var t = authService.IsTokenValid(token);

            //TODO actually validate the role here 
            var stream = token;
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = jsonToken as JwtSecurityToken;
            var jti = tokenS.Claims.First(claim => claim.Type == "role").Value;

            if (!t)
            {
                base.HandleUnauthorizedRequest(actionContext);
            }
            else
            {
                base.IsAuthorized(actionContext);
            }
        }

        private static JWTContainerModel GetJWTContainerModel(string name, string email)
        {
            return new JWTContainerModel()
            {
                Claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, name),
                    new Claim(ClaimTypes.Email, email)
                }
            };
        }
    }
}