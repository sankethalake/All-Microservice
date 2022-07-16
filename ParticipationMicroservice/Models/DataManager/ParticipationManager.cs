using Microsoft.EntityFrameworkCore;
using ParticipationMicroservice.DBContexts;
using ParticipationMicroservice.Model;
using ParticipationMicroservice.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParticipationMicroservice.Models.DataManager
{
    public class ParticipationManager: IDataRepository<Participation>
    {
        readonly ParticipationContext _participationContext;
        static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(ParticipationManager));
        public ParticipationManager(ParticipationContext context)
        {
            _participationContext = context;
        }

        //method to get all participation
        public IEnumerable<Participation> GetAll()
        {
           var data = _participationContext.Participations.ToList();

            return data;
        }

        //method to get participation by id
        public Participation Get(long id)
        {
            return _participationContext.Participations
                  .FirstOrDefault(p => p.ParticipationId == id);
        }

        //method to get participation by status
        public IEnumerable<Participation> GetByStatus(string status)
        {
            IEnumerable<Participation> participantList= null;
            
                if (Enum.TryParse(status, true, out ParticipationStatus enumStatus))
                {
                    participantList = _participationContext.Participations.Where(s => s.Status == enumStatus);
                }

    
            return participantList;
        }


        //method to add participation
        public bool Add(Participation entity)
        {
            bool flag = false;
            if (Get(entity.ParticipationId) != null)
            {
                return flag;
            }
            try
            {   
                //checks whether the object is present or not
                _participationContext.Entry(entity).State = EntityState.Detached;
                var existingEvent = _participationContext.Events.Local.SingleOrDefault(e => e.EventId == entity.Events.EventId);
                if (existingEvent != null)
                {
                    entity.Events = existingEvent;
                    _participationContext.Entry(existingEvent).State = EntityState.Detached;
                }
                _participationContext.Attach(entity);
                
                //for (int iter = 0; iter < entity.Player.Count; iter++)
                //{
                //    _participationContext.Entry(entity).State = EntityState.Detached;
                //    //_participationContext.Entry(entity.Player).State = EntityState.Detached;
                //    Player player = entity.Player.ElementAt(iter);
                //    //_participationContext.Entry(entity.Player.ElementAt(iter)).State = EntityState.Detached;
                //    var existingSports = _participationContext.Sports.Local.SingleOrDefault(e => e.SportId == player.Sports.SportId);
                //    if (existingSports != null)
                //    {
                //        entity.Player.ElementAt(iter).Sports = existingSports;
                //        _participationContext.Entry(existingSports).State = EntityState.Detached;
                //    }
                //    //_participationContext.Attach(entity.Player.ElementAt(iter));
                //    _participationContext.Attach(entity);
                //}


                //_participationContext.Entry(entity).State = EntityState.Modified;
                _participationContext.Participations.Add(entity);
                _participationContext.SaveChanges();
                flag = true;
            }
            catch(Microsoft.EntityFrameworkCore.DbUpdateException e)
            {
                _logger.Error(e.Message);
            }
            catch (System.InvalidOperationException e)
            {
                _logger.Error(e.Message);
            }

            return flag;
        }

        //method to update status of participation
        public bool Update(Participation participation, string status)
        {
            if (Enum.TryParse(status, true, out ParticipationStatus enumStatus)){
                participation.Status = enumStatus;
                _participationContext.SaveChanges();
                return true;
            }
            return false;
        }



    }
}
