using JwtAuthentication.Database;
using JwtAuthentication.Repositories;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static JwtAuthentication.Database.Login;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JwtAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        private readonly IRepository<Login> loginRepository;
        private readonly ILog log;

        public LoginController(IJwtAuthenticationManager jwtAuthenticationManager,IRepository<Login> loginRepository)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
            this.loginRepository = loginRepository;
            this.log = LogManager.GetLogger(typeof(LoginController));
        }
        // GET: api/<LoginController>
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    IEnumerable<Login> logins = loginRepository.GetAll();
        //    return Ok(logins);
        //}


        [AllowAnonymous]
        [HttpPost, Route("authenticate")]
        public IActionResult Authenticate([FromBody] Login userCred)
        {
            if (!loginRepository.Get(userCred))
            {
                
                log.Warn("Invalid User Login");
                log.Error("401 Unauthorized Error");
                return Unauthorized(null);
            }

            var tokenString = jwtAuthenticationManager.GenerateToken(userCred.Username, userCred.Password);
            return Ok(new { token = tokenString });
        }

        [AllowAnonymous]
        [HttpPost, Route("Create")]
        public IActionResult CreateUser([FromBody] Login userCred)
        {
            Login login = loginRepository.CreateLogin(userCred);
            if (login == null) return BadRequest("Username is already exists");
            return Ok(new {msg = "User registered successfully" });
        }
    }
}
