using Saiyan.Domain.Entities;
using Saiyan.Domain.Models;

namespace Saiyan.Mapper
{
    public interface IMapper<T, J>
        where T : BaseModel
        where J : BaseEntity
    {
        J ToEntity(T model);
        T ToModel(J entity);
    }
}
