using Entity = Saiyan.Domain.Entities;
using Saiyan.Domain.Models;

namespace Saiyan.Mapper
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class PersonMapper<T, J> : IMapper<Person, Entity.Person>
    {
        public Entity.Person ToEntity(Person model)
        {
            var personEntity = new Domain.Entities.Person();
            personEntity.id = model.id;
            personEntity.name = model.name;
            return personEntity;
        }

        public Person ToModel(Entity.Person entity)
        {
            var person = new Person();
            person.id = entity.id;
            person.name = entity.name;
            return person;
        }
    }
}
