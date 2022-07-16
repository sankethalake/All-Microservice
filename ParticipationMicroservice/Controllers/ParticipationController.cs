using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParticipationMicroservice.Model;
using ParticipationMicroservice.Models;
using ParticipationMicroservice.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ParticipationMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ParticipationController : ControllerBase
    {
        private readonly IDataRepository<Participation> _dataRepository;
        static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(ParticipationController));
        public ParticipationController(IDataRepository<Participation> dataRepository)
        {
            _dataRepository = dataRepository;
        }
        // GET: api/Participation
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Participation> participation = _dataRepository.GetAll();
            return Ok(participation);
        }

        // GET: api/Participation/getPendingParticipations
        [HttpGet("getPendingParticipations/{status}", Name = "GetByPending")]
        public IActionResult GetByPending(string status)
        {
            if(!status.ToLower().Equals("pending"))
            {
                return BadRequest("Enter valid status");
            }
            IEnumerable<Participation> participation = _dataRepository.GetByStatus(status);
            return Ok(participation);
        }

        // GET: api/Participation/getApprovedParticipations 
        [HttpGet("getApprovedParticipations/{status}", Name = "GetByApproved")]
        public IActionResult GetByApproved(string status)
        {
            if (!status.ToLower().Equals("approved"))
            {
                return BadRequest("Enter valid status");
            }
            IEnumerable<Participation> participation = _dataRepository.GetByStatus(status);
            return Ok(participation);
        }

        // GET: api/Participation/GetByDeclined 
        [HttpGet("getDeclinedParticipations/{status}", Name = "GetByDeclined")]
        public IActionResult GetByDeclined(string status)
        {
            if (!status.ToLower().Equals("declined"))
            {
                return BadRequest("Enter valid status");
            }
            IEnumerable<Participation> participation = _dataRepository.GetByStatus(status);
            return Ok(participation);
        }


        // POST: api/Participation
        [HttpPost]
        public IActionResult Post([FromBody] Participation participation)
        {
            // validates participation object
            if (participation == null)
            {
                _logger.Warn("Invalid Participation object tried to create");
                return BadRequest("Employee is null.");               
            }
            if(_dataRepository.Add(participation)) return Ok(participation);
            _logger.Warn("Participation Object is already present");
            return BadRequest("SQL EXCEPTION: (Hint)Object is already present");
        }


        // PUT: api/Participation/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody] StatusModel statusModel)
        {
            Participation participationToUpdate = _dataRepository.Get(id);

            // checks participationToUpdate is valid Participation object or not
            if (participationToUpdate == null)
            {
                _logger.Warn("Participation to be changed not found");
                return NotFound("Participation not found");
            }

            //validates status
            if(!_dataRepository.Update(participationToUpdate, statusModel.Status))
            {
                _logger.Warn("Invalid status in the request");
                return BadRequest("Select Valid Status");
            }           
            return Ok(statusModel+" successfully");     
        }

    }
}
