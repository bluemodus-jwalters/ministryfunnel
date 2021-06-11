using Newtonsoft.Json;
using RestSharp;
using System;
using System.Configuration;
using System.Globalization;
using System.Web;

namespace MinistryFunnel.FrontEnd.Helpers
{
    class AuthenticationModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string api_user { get; set; }
        public string email { get; set; }
    }
    public class ApiHelper : IApiHelper
    {
        private string _token;

        private string CompileUrl(string action)
        {
            return ConfigurationManager.AppSettings["ApiBaseUrl"] + action;
        }
        private string GenerateToken()
        {
            
            try
            {
                var body = new AuthenticationModel { api_user = "api_user", username = "Jordan Walters", email = "jordanwalters@discoverychurch.org", password = "password" };
                var client = new RestClient(CompileUrl("/api/token"));
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                var x = JsonConvert.SerializeObject(body);
                request.AddParameter("application/json", x, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                return response.Content.ToString();
            }
            catch (Exception e)
            {
                var t = e;
            }

            return null;
        }


        private string ReturnToken(HttpRequestBase httpRequestBase, HttpResponseBase httpResponseBase)
        {
            System.Web.HttpCookie aCookie = httpRequestBase.Cookies["MinistryFunnelToken"];
            if (aCookie != null)
            {

                string token = aCookie["MinistryFunnelToken"];
                return token;
            }
            else
            {
                //refactor this into it's own method or something
                var newToken = GenerateToken();
                System.Web.HttpCookie cookie = new System.Web.HttpCookie("MinistryFunnelToken");
                cookie["MinistryFunnelToken"] = newToken;
                cookie.Expires = DateTime.Now.AddMinutes(10); //TODO: increase timeout
                httpResponseBase.Cookies.Add(cookie);
                return newToken;
            }
        }

        public string GetTokenPublic(HttpRequestBase httpRequestBase, HttpResponseBase httpResponseBase)
        {
            return ReturnToken(httpRequestBase, httpResponseBase);
        }

        public IRestResponse Delete(string url, object json)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return response;
        }

        public IRestResponse Get(string url, HttpRequestBase httpRequestBase, HttpResponseBase httpResponseBase)
        {
            var token = ReturnToken(httpRequestBase, httpResponseBase);
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", "token");
            IRestResponse response = client.Execute(request);
            return response;
        }

        public IRestResponse Get(string url, string token)
        {

            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", token);
            IRestResponse response = client.Execute(request);
            return response;
        }

        public IRestResponse Get(string url, string parameterKey, object parameterValue)
        {
            var client = new RestClient(url);

            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddParameter(parameterKey, parameterValue);
            IRestResponse response = client.Execute(request);
            return response;
        }

        public IRestResponse Post(string url, string json)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return response;
        }

        public IRestResponse Put(string url, string json)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            return response;
        }
    }
}