using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Npgsql;

namespace FSGEMappingTool.Models.Repositories
{
    public class RepositoryBase
    {
        public IDbConnection GetConnection()
        {
            //uses Postgres Connection
            return new NpgsqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString);
        }
    }
}