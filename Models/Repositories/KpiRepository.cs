using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using Dapper;
using FSGEMappingTool.Models.Domain;

namespace FSGEMappingTool.Models.Repositories
{
    public class KpiRepository : RepositoryBase
    {

        private readonly string columnSet = " k.*, c.address, c.city, c.state, c.zip ";

        public IList<Kpi> GetAll()
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                var items = conn.Query<Kpi>("select * from adaptdev_datasource.kpi_list");
                return items.ToList();
            }
        }

        public IEnumerable<dynamic> GetAllKpisForClient(string clientId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                DynamicParameters args = new DynamicParameters();
                args.Add("clientId", clientId);
                var data = conn.Query("select " + columnSet + " from adaptdev_datasource.kpi_by_column k where k.client_id=@clientId LIMIT 100", args);
                return data;
            }
        }

        public IEnumerable<dynamic> GetTopAndBottomKpisForClientByKPI(string clientId, string clientTableName, string rankColumn, string recordLimit)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                DynamicParameters args = new DynamicParameters();
                args.Add("clientId", clientId);
                //args.Add("rankColumn", rankColumn); //if i try to use an ARG - driver puts single quotes around param and gives me a non-
                args.Add("limit", recordLimit);

                string query = "(select " + columnSet + ", TRUE as \"istop\" from adaptdev_datasource.kpi_by_column k, adaptdev_datasource." + clientTableName + " c where k.site_id=c.site_id AND k.client_id=@clientId ORDER BY " + rankColumn + " ASC LIMIT @limit)";
                query += " UNION ALL ";
                query += "(select " + columnSet + ", False as \"istop\" from adaptdev_datasource.kpi_by_column k, adaptdev_datasource." + clientTableName + " c where k.site_id=c.site_id AND k.client_id=@clientId ORDER BY " + rankColumn + " DESC LIMIT @limit)";

                var data = conn.Query(query, args);
                return data;
            }
        }

        public IEnumerable<dynamic> GetKpisForClientSite(string clientId, string siteId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                DynamicParameters args = new DynamicParameters();
                args.Add("clientId", clientId);
                args.Add("siteId", clientId);
                var data = conn.Query("select " + columnSet + " from adaptdev_datasource.kpi_by_column where client_id=@clientId AND site_id=@siteId", args);
                return data;
            }
        }

    }
}