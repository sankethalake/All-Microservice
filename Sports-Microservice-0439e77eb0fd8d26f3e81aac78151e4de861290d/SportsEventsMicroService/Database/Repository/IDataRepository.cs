using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsEventsMicroService.Database.Repository
{
    public interface IDataRepository<TEntity>
    {

        
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        TEntity GetByName(string name);
        bool Add(TEntity entity);
        bool Delete(TEntity entity);
        //void Delete(int id);
    }
}
