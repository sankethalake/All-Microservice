using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerMicroService.Repositories
{
    public interface IDataRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        bool Delete(int id);
    }


}
