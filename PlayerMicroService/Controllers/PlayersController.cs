using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicroService2.Database;
using PlayerMicroService.Repositories;
using log4net;
using Microsoft.AspNetCore.Authorization;

namespace PlayerMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlayersController : ControllerBase
    {
        private readonly IDataRepository<Player> _playerRepository;
        private readonly ILog _logger;

        public PlayersController(IDataRepository<Player> playerRepository)
        {
            this._playerRepository = playerRepository;
            this._logger = LogManager.GetLogger(typeof(PlayersController));
        }

        // GET: api/Players
        [HttpGet]
        public  IActionResult GetPlayers()
        {
            IEnumerable<Player> players=_playerRepository.GetAll();
            return Ok(players);
        }

        // GET: api/Players/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Player>> GetPlayer(int id)
        //{
        //    var player = await _context.Players.FindAsync(id);

        //    if (player == null)
        //    {
        //        return NotFound();
        //    }

        //    return player;
        //}

        // PUT: api/Players/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutPlayer(int id, Player player)
        //{
        //    if (id != player.PlayerId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(player).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PlayerExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Players
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public IActionResult PostPlayer([FromBody] Player player)
        {
            _playerRepository.Add(player);

            return Ok(player);
        }

        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        public IActionResult DeletePlayer(int id)
        {
            
            if (!_playerRepository.Delete(id))
            {
                _logger.Warn("Player Doesn't exist");
                return BadRequest("Player not found");
            }
            
            return Ok( new { message = $"Player with id {id} deleted successfully" } );

        }


        ////private bool PlayerExists(int id)
        ////{
        ////    return _context.Players.Any(e => e.PlayerId == id);
        ////}
    }
}
