using Saiyan.Domain.Entities;
using Saiyan.Domain.Models;
using Saiyan.Mapper;
using Saiyan.Repository;

namespace Saiyan.Components
{
    public abstract class BaseComponent<T, J>
        where T : BaseModel
        where J : BaseEntity
    {
        internal IMapper<T, J> mapper;
        internal IRepository<J> repository;

        public BaseComponent(IRepository<J> _repository, IMapper<T, J> _mapper)
        {
            mapper = _mapper;
            repository = _repository;
        }
    }
}
