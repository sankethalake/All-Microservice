using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using SportsEventsMicroService.Database;
using SportsEventsMicroService.Database.Repository;
using SportsEventsMicroService.Database.DataManager;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace SportsEventsMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SportsController : ControllerBase
    {
        private readonly ISportDataRepository<Sport> _dataRepository;
        static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(SportsController));
        public SportsController(ISportDataRepository<Sport> dataRepository)
        {
            _dataRepository = dataRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Sport> sport = _dataRepository.GetAll();
            return Ok(sport);
        }

        //Commented because not required
        //[HttpGet("{id}",Name ="GetSport")]
        //public IActionResult Get(int id)
        //{
        //    Sport sport = _dataRepository.Get(id);
        //    if(sport == null)
        //    {
        //        _logger.Warn("Searching Non Existent Sport.");
        //        return NotFound("Not Found");
        //    }
        //    return Ok(sport);
        //}


        [HttpGet("GetByName/{name}", Name = "GetSportByName")]
        public IActionResult GetByName(string name)
        {
            Sport sport = _dataRepository.GetByName(name);
            if (sport == null)
            {
                _logger.Warn("Searching Non Existent Sport.");
                return NotFound($"{name} Not Found");
            }
            return Ok(sport);

        }


        // Commented because not required
        //[HttpPost]
        //public IActionResult Post([FromBody] Sport sport)
        //{
        //    if (sport==null)
        //    {
        //        _logger.Warn("Entering NULL Data");
        //        return BadRequest("Employee is null");
        //    }
        //    _dataRepository.Add(sport);
        //    return CreatedAtRoute(
        //        "Get",
        //        new { Id = sport.SportId },
        //        sport);
        //}
        //[HttpDelete("{id}")]
        //public IActionResult Delete(int id)
        //{
        //    Sport sport = _dataRepository.Get(id);
        //    if (sport == null)
        //    {
        //        _logger.Warn("Deleting Non Existent Event.");
        //        return NotFound("Sport not found");
        //    }
        //    _dataRepository.Delete(sport);
        //    return Ok(sport);
        //}
    }
}
