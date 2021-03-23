using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinistryFunnel.FrontEnd.Helpers
{
    interface IApiHelper
    {
        IRestResponse Get(string url);

        //TODO: make this an array
        IRestResponse Get(string url, string parameterKey, object parameterValue);

        IRestResponse Post(string url, string json);

        IRestResponse Put(string url, string json);

        IRestResponse Delete(string url, object json);
    }
}
