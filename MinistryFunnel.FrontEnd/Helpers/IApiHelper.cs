using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MinistryFunnel.FrontEnd.Helpers
{
    public interface IApiHelper
    {
        IRestResponse Get(string url, string token);
        //TODO: make this an array
        //IRestResponse Get(string url, HttpRequestBase httpRequestBase, HttpResponseBase httpResponseBase);
        IRestResponse Get(string url, string parameterKey, object parameterValue, string token);

        IRestResponse Post(string url, string json);

        IRestResponse Post(string url, string json, string token);

        IRestResponse Put(string url, string json);
        IRestResponse Put(string url, string json, string token);

        IRestResponse Delete(string url, object json);
        IRestResponse Delete(string url, object json, string token);

        string GetTokenPublic(HttpRequestBase httpRequestBase, HttpResponseBase httpResponseBase, string username, string email);
    }
}
