using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportsEventsMicroService.Database;
using SportsEventsMicroService.Database.Repository;

namespace SportsEventsMicroService.Database.DataManager
{
    public class SportsManager : ISportDataRepository<Sport>
    {
        readonly DatabaseContext _Sportcontext;
        static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(SportsManager));
        public SportsManager(DatabaseContext sportcontext)
        {
            _Sportcontext = sportcontext;
        }

        public IEnumerable<Sport> GetAll()
        {
            return _Sportcontext.Sports.ToList();
        }

        public Sport Get(int id)
        {
            return _Sportcontext.Sports.FirstOrDefault(e => e.SportId == id);
        }

        // Commented because not required
        //public void Add(Sport entity)
        //{
        //    _Sportcontext.Sports.Add(entity);
        //    _Sportcontext.SaveChanges();
        //}

        // Commented Because not required
        //public void Delete(Sport sport)
        //{
        //    _Sportcontext.Sports.Remove(sport);
        //    _Sportcontext.SaveChanges();
        //}

        public Sport GetByName(string name)
        {
            return _Sportcontext.Sports.FirstOrDefault(e => e.SportName == name);
        }
    }
}
