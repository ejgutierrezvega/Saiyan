using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Saiyan.Repository;
using Saiyan.Mapper;
using Saiyan.Components;
using Entity = Saiyan.Domain.Entities;
using Saiyan.Domain.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Saiyan.Api.Controllers
{
    [Route("api/[controller]")]
    public class PersonController : BaseController<Person, Entity.Person>
    {
        public PersonController(IRepository<Entity.Person> repository, IMapper<Person, Entity.Person> mapper, IComponent<Person> component, IIdEngine idEngine)
            : base(repository, mapper, component, idEngine) { }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var people = component.Get();

            return Ok(people);
        }

        // GET: api/values
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var person = component.GetById(id);

            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person model)
        {
            model.id = idEngine.NewGuid();

            component.Add(model);

            return Created(new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}/{model.id}"), null);
        }
    }
}
