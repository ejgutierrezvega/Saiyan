using Entity = Saiyan.Domain.Entities;
using Saiyan.Domain.Models;
using Saiyan.Mapper;
using Saiyan.Repository;
using System.Collections.Generic;
using System;

namespace Saiyan.Components
{
    public class PersonComponent : BaseComponent<Person, Entity.Person>, IComponent<Person>
    {
        public PersonComponent(IRepository<Entity.Person> repository, IMapper<Person, Entity.Person> mapper) 
            :base(repository, mapper) { }

        public IEnumerable<Person> Get()
        {
            var personEntities = repository.ToList(r => true);
            var people = new List<Person>();

            foreach(var personEntity in personEntities)
                people.Add(mapper.ToModel(personEntity));

            return people;
        }

        public Person GetById(Guid id)
        {
            var personEntity = repository.First(r => r.id == id);
            var person = mapper.ToModel(personEntity);

            return person;
        }


        public void Add(Person model)
        {
            var personEntity = mapper.ToEntity(model);
            repository.Insert(personEntity);
        }
    }
}
