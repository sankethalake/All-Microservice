using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SportsEventsMicroService.Database;
using SportsEventsMicroService.Database.Repository;
using SportsEventsMicroService.Database.DataManager;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;

namespace SportsEventsMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventsController : ControllerBase
    {
        private readonly IDataRepository<Event> _dataRepository;
        static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(EventsController));
        public EventsController(IDataRepository<Event> dataRepository) 
        {
            _dataRepository = dataRepository;

        }
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Event> events = _dataRepository.GetAll();
            return Ok(events);
            
        }


        
        //[HttpGet("{id}", Name = "Get")]
        //public IActionResult Get(int id)
        //{
        //    Event events = _dataRepository.Get(id);
        //    if (events == null)
        //    {
        //        _logger.Warn("Searching Non Existent Event.");
        //        return NotFound("Not Found");
        //    }
        //    return Ok(events);
        //}


        [HttpGet("GetByName/{name}", Name = "GetEventByName")]
        public IActionResult GetByName(string name)
        {
            Event events = _dataRepository.GetByName(name);
            if (events == null)
            {
                _logger.Warn("Searching Non Existent Event.");
                return NotFound($"{name} Event Not Found");
            }
            return Ok(events);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Event events)
        {
            if (events == null)
            {
                _logger.Warn("Entering NULL Data.");
                return BadRequest("Event is null");
            }
            _dataRepository.Add(events);
            return CreatedAtRoute(
                "Get",
                new { Id = events.EventId },
                events);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Event events = _dataRepository.Get(id);
            if (events == null)
            {
                _logger.Warn("Deleting Non Existent Event.");
                return NotFound("Event not found");
            }
        _dataRepository.Delete(events);
            return Ok(new { message = $"Event with id {id} deleted successfully" });
        }
    }
}
