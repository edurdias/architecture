using System;
using System.Configuration;
using RestSharp;

namespace AdventureWorks.Service.Api.Proxy
{
    public class ProxyBase
    {
        public ProxyBase()
        {
            Endpoint = ConfigurationManager.AppSettings["Service:Endpoint"];
            Client = new RestClient(Endpoint);
        }

        public string Endpoint { get; set; }
        public RestClient Client { get; set; }

        protected T Execute<T>(RestRequest request) where T : new()
        {
            var response = Client.Execute<T>(request);
            if (typeof (T).IsValueType)
                return (T) Convert.ChangeType(response.Content, typeof(T));
            return response.Data;
        }

        protected void Execute(RestRequest request)
        {
            Client.Execute(request);
        }
    }
}