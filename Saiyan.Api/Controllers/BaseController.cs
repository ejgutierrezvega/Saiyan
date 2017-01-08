using Microsoft.AspNet.Mvc;
using Saiyan.Components;
using Saiyan.Domain.Entities;
using Saiyan.Domain.Models;
using Saiyan.Mapper;
using Saiyan.Repository;

namespace Saiyan.Api.Controllers
{
    public abstract class BaseController<T, J> : Controller
        where T: BaseModel
        where J: BaseEntity
    {
        internal IMapper<T, J> mapper;
        internal IRepository<J> repository;
        internal IComponent<T> component;
        internal IIdEngine idEngine;

        public BaseController(IRepository<J> _repository, IMapper<T, J> _mapper, IComponent<T> _component, IIdEngine _idEngine)
        {
            repository = _repository;
            mapper = _mapper;
            component = _component;
            idEngine = _idEngine;
        }
    }
}
