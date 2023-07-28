using MinistryFunnel.FrontEnd.Helpers;
using MinistryFunnel.FrontEnd.Service;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace MinistryFunnel.FrontEnd.Controllers
{
    public class BaseController : Controller
    {
        public readonly IApiHelper _apiHelper;
        private readonly ILoggerService _logger;


        public BaseController()
        {
            _apiHelper = new ApiHelper();
            _logger = new LoggerService();
            
        }

        protected string GetToken()
        {

            if (this.Session["token"] == null || this.Session["token"].ToString().Length == 0)
            {
                var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;
                //check to see if token was null coming back from the server and redirect to error page is nothing is happening
                this.Session["token"] = GenerateToken(ReturnUserName(userClaims), ReturnEmail(userClaims));
            }

            var token = this.Session["token"].ToString();

            if (token == null)
            {
                //return error page
            }

            return token;
        }

        private string ReturnUserName(ClaimsIdentity userClaims)
        {
            var username = userClaims?.FindFirst("name")?.Value;

            if (userClaims == null)
            {
                _logger.CreateLog("unidentified", "Ministry Calendar Front End", "BaseController", "ReturnUserName", "ReturnUserName", "", "User claims was not returned when retrieving the token");
                return "default";
            }

            if (username == null || username.ToString().Length == 0)
            {
                _logger.CreateLog("unidentified", "Ministry Calendar Front End", "BaseController", "ReturnUserName", "ReturnUserName", "", "Username was not returned when retrieving the token");
                return "default";
            }

            return username;
        }

        private string ReturnEmail(ClaimsIdentity userClaims)
        {
            var email = userClaims?.FindFirst("preferred_username")?.Value;

            if (userClaims == null)
            {
                _logger.CreateLog("unidentified", "Ministry Calendar Front End", "BaseController", "ReturnEmail", "ReturnEmail", "", "User claims was not returned when retrieving the token");
                return "default@test.com";
            }

            if (email == null || email.ToString().Length == 0)
            {
                _logger.CreateLog("unidentified", "Ministry Calendar Front End", "BaseController", "ReturnEmail", "ReturnEmail", "", "Email was not returned when retrieving the token");
                return "default@test.com";
            }

            return email;
        }

        private string GenerateToken(string username, string email)
        {
            try
            {
                var body = new AuthenticationModel { api_user = ConfigurationManager.AppSettings["token_username"], username = username, email = email, password = ConfigurationManager.AppSettings["token_password"] };
                var client = new RestClient(CompileUrl("/api/token"));
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                var x = JsonConvert.SerializeObject(body);
                request.AddParameter("application/json", x, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                var token = JsonConvert.DeserializeObject(response.Content);
                return token.ToString();
            }
            catch (Exception e)
            {
                var t = e;
            }

            return null;
        }

        private string CompileUrl(string action)
        {
            return ConfigurationManager.AppSettings["ApiBaseUrl"] + action;
        }

    }
}