using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AdventureWorks.Domain.Contracts;
using AdventureWorks.Foundation;
using AdventureWorks.Service.Api.ViewModels.Person;
using Foundation.Services;

namespace AdventureWorks.Service.Api.Controllers
{
    public class PersonController : ApiController, ITranslator<IPerson, PersonViewModel>
    {
        public PersonController()
        {
            Domain = Ioc.Resolve<IPerson>();
        }

        public IPerson Domain { get; set; }

        [HttpGet]
        [Route("People/CountAll")]
        public int CountAll()
        {
            return Domain.CountAll();
        }

        [HttpGet]
        [Route("People/{skip:int}/{take:int}")]
        public IEnumerable<PersonViewModel> GetAll(int skip = 0, int take = Int32.MaxValue)
        {
            return Domain.GetAll(skip, take).Select(Translate);
        }

        [HttpGet]
        [Route("People/{id:int}")]
        public PersonViewModel Load(int id)
        {
            var domain = Domain.Load(id);
            return domain == null ? null : Translate(domain);
        }

        [HttpPost]
        [Route("People")]
        public void Save(PersonViewModel model)
        {
            var domain = Translate(model);
            domain.Save();
        }

        [HttpDelete]
        [Route("People/{id:int}")]
        public void Remove(int id)
        {
            var domain = Domain.Load(id);
            domain.Remove();
        }

        public PersonViewModel Translate(IPerson instance)
        {
            if (instance == null)
                return null;

            return new PersonViewModel
            {
                Id = instance.Id,
                Type = instance.Type,
                FirstName = instance.FirstName,
                LastName = instance.LastName
            };
        }

        public IPerson Translate(PersonViewModel instance)
        {
            if (instance == null)
                return null;

            var domain = Ioc.Resolve<IPerson>();
            if (instance.Id != null) 
                domain.Id = instance.Id.Value;

            domain.Type = instance.Type;
            domain.FirstName = instance.FirstName;
            domain.LastName = instance.LastName;
            return domain;
        }
    }
}
