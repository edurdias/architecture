using System;
using System.Collections.Generic;
using System.Linq;
using AdventureWorks.Domain.Contracts;
using RestSharp;

namespace AdventureWorks.Service.Api.Proxy
{
    public class PersonProxy : ProxyBase, IPerson
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime ModifiedDate { get; set; }

        public int CountAll()
        {
            return Execute<int>(new RestRequest("People/CountAll", Method.GET));
        }

        public IList<IPerson> GetAll(int skip = 0, int take = Int32.MaxValue)
        {
            var request = new RestRequest("People/{skip}/{take}", Method.GET)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddParameter("skip", skip, ParameterType.UrlSegment);
            request.AddParameter("take", take, ParameterType.UrlSegment);
            return Execute<List<PersonProxy>>(request).OfType<IPerson>().ToList();
        }

        public IPerson Load(int id)
        {
            var request = new RestRequest("People/{id}", Method.GET);
            request.AddParameter("id", id, ParameterType.UrlSegment);
            return Execute<PersonProxy>(request);
        }

        public void Save()
        {
            var request = new RestRequest("People", Method.POST)
            {
                RequestFormat = DataFormat.Json
            };
            request.AddJsonBody(this);
            Execute(request);
        }

        public void Remove()
        {
            var request = new RestRequest("People/{id}", Method.DELETE);
            request.AddParameter("id", Id, ParameterType.UrlSegment);
            Execute(request);
        }
    }
}
