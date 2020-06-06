using Dapper;
using eCNES.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace eCNES.Repository
{
    public class UsuarioRepository
    {
        private readonly IConfiguration _configuration;

        public UsuarioRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public User Authenticate(User model)
        {
            var cn = Conexao.GetInstance(_configuration).GetConnection;
            var user = GetAll().Where(x => x.txt_login == model.Username.Trim() && x.txt_senha == model.Password).FirstOrDefault();                 

            if (user == null)
                return null;

            model.Expires = DateTime.UtcNow.AddHours(2);

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.txt_login)                    
                }),
                Expires = model.Expires,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            model.Token = tokenHandler.WriteToken(token);            
            model.Password = null;            

            return model;
        }

        public IEnumerable<Usuario> GetAll()
        {            
            var cn = Conexao.GetInstance(_configuration).GetConnection;            
            return cn.Query<Usuario>("SELECT * FROM tbl_usuario"); 
        }

        public Usuario Get(int id)
        {
            var cn = Conexao.GetInstance(_configuration).GetConnection;
            var retorno = cn.QueryFirstOrDefault<Usuario>(
                "SELECT * FROM tbl_usuario WHERE cod_usuario = @cod_usuario",
                new { cod_usuario = id }
                );
            
            return retorno;
        }
    }
}
