using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using Dapper;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace eCNES.Repository
{
    public class Conexao
    {
        private static Conexao s_objDBConnect;
        private static IDbConnection s_objConnection;
        private readonly IConfiguration _configuration;

        public Conexao(IConfiguration configuration)
        {
            _configuration = configuration;

            s_objConnection = new NpgsqlConnection();
            s_objConnection.ConnectionString = _configuration.GetConnectionString("DefaultConnection");                
            s_objConnection.Open();

            s_objConnection.Execute("SET search_path TO " + _configuration.GetSection("AppSettings").GetSection("schema").Value);
            s_objConnection.Execute("SET TIMEZONE TO 'America/Sao_Paulo'");
        }
        

        public static Conexao GetInstance(IConfiguration configuration)
        {
            if (s_objDBConnect == null)
            {
                s_objDBConnect = new Conexao(configuration);
                return s_objDBConnect;
            }
            if (s_objDBConnect.GetConnection.State == ConnectionState.Closed)
            {
                s_objDBConnect = new Conexao(configuration);
                return s_objDBConnect;
            }

            return s_objDBConnect;
        }

        public IDbConnection GetConnection
        {
            get
            {
                return s_objConnection;
            }
        }
    }
}
