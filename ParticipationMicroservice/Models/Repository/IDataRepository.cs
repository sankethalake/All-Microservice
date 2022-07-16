using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParticipationMicroservice.Models.Repository
{
    public interface IDataRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(long id);
        IEnumerable<TEntity> GetByStatus(string status);
        bool Add(TEntity entity);
        bool Update(TEntity dbEntity, string status);
    }
}
