using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinistryFunnel.FrontEnd.Helpers
{
    public class ApiHelper : IApiHelper
    {
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

        public IRestResponse Get(string url)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
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