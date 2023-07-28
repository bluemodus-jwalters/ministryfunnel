using AuthenticationService.Managers;
using AuthenticationService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        //Can make this an array of needed: https://stackoverflow.com/questions/5117782/how-to-extend-authorizeattribute-and-check-the-users-roles
        public string Role { get; set; }
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //If needed, we can get the roles from the actionContext that is passed in via the attribute roles
            IAuthService authService = new JWTService(ConfigurationManager.AppSettings["ApiSecretKey"]);

            string token = string.Empty;
            IEnumerable<string> values;
            actionContext.Request.Headers.TryGetValues("Authorization", out values);
            if (values != null)
            {
                token = values.FirstOrDefault();
            }
            
            if (token.Length > 0)
            {
                var validToken = authService.IsTokenValid(token);

                if (!validToken)
                {
                    base.HandleUnauthorizedRequest(actionContext);
                }
                else
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jwtToken = handler.ReadJwtToken(token);
                    HttpContext.Current.Items["email"] = jwtToken.Claims.Where(x => x.Type == "email").FirstOrDefault().Value;


                    //roles are not being generated right now in the token controller
                    //var roles = from claim in jwtToken.Claims
                    //        where claim.Type == "discovery_api_edit"
                    //            select claim.Value;

                    //if (roles.Contains(Role))
                    //{
                    //    base.IsAuthorized(actionContext);
                    //}
                    base.IsAuthorized(actionContext);
                    //base.HandleUnauthorizedRequest(actionContext);
                }
            }
            else
            {
                base.HandleUnauthorizedRequest(actionContext);
            }

            
            
        }
    }
}