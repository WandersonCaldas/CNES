using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCNES.Models;
using eCNES.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace eCNES.Controllers
{    
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("token")]
        [AllowAnonymous]
        public ActionResult<User> Login([FromBody] User model)
        {
            try
            {
                var service = new UsuarioRepository(_configuration);
                var user = service.Authenticate(model);

                if (user == null)
                    return BadRequest(new { message = "Usuário ou senha inválidos" });

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/<UsuarioController>
        [HttpGet]       
        public ActionResult<IEnumerable<Usuario>> Get()
        {
            var retorno = new UsuarioRepository(_configuration).GetAll();
            if (retorno == null)
            {
                return NotFound(StatusCodes.Status404NotFound);
            }
            foreach (var x in retorno)
            {
                x.txt_senha = null;
            }
            return Ok(retorno);
        }
        
        // GET api/<UsuarioController>/5        
        [HttpGet("{id}")]
        public ActionResult<Usuario> Get(int id)
        {            
            var retorno = new UsuarioRepository(_configuration).Get(id);

            if (retorno == null)
            {
                return NotFound();
            }

            retorno.txt_senha = null;
            return Ok(retorno);
        }
    }
}
