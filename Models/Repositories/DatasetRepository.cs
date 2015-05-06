using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Dapper;
using FSGEMappingTool.Models.Domain;

namespace FSGEMappingTool.Models.Repositories
{
    public class DatasetRepository : RepositoryBase
    {
        public IEnumerable<dynamic> GetClientDataset(string clientTableName)
        {
            //using (var conn = GetConnection())
            //{
            //    conn.Open();
            //    DynamicParameters args = new DynamicParameters();
            //    args.Add("clientTable", clientTableName);
            //    var data = conn.Query("select * from adaptdev_datasource." + clientTableName + " Limit 100", args);
            //    return data;
            //}

            return GetClientDataset(clientTableName, "9999");
        }

        public IEnumerable<dynamic> GetClientDataset(string clientTableName, string limit)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                DynamicParameters args = new DynamicParameters();
                args.Add("clientTable", clientTableName);
                var data = conn.Query("select * from adaptdev_datasource." + clientTableName + " Limit " + limit, args);
                return data;
            }
        }

        public IEnumerable<DBColumn> GetColumnsForClientDBTable(string clientTableName)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                DynamicParameters args = new DynamicParameters();
                args.Add("clientTable", clientTableName);
                var data = conn.Query<DBColumn>("SELECT column_name FROM information_schema.columns WHERE table_name =@clientTable", args);
                return from x in data
                    where x.ColumnName != "client_id"
                    select x;
            }
        }

        public IEnumerable<ClientColumnConfig> GetClientColumnConfigs(string clientid)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                DynamicParameters args = new DynamicParameters();
                args.Add("clientId", clientid);
                var data = conn.Query<ClientColumnConfig>("SELECT * FROM adaptdev_datasource.client_column_configurations WHERE client_id=@clientId Order By order_index ASC", args);
                
                return data;
            }
        }

        public IEnumerable<dynamic> GetTopFilterValues(string clientTableName, string filterColumnName, string limit)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                DynamicParameters args = new DynamicParameters();
                args.Add("clientTable", clientTableName);
                args.Add("column", filterColumnName);
                args.Add("column2", filterColumnName);
                args.Add("limit", limit); 
                //var data = conn.Query("SELECT @column as filter_value, count(@column2) as item_count FROM adaptdev_datasource.[@clientTable] Group By filter_value Order By item_count Desc Limit @limit", args);
                //driver bombs out if you use table name as a param
                var data = conn.Query("SELECT " + filterColumnName + " as filter_value, count(" + filterColumnName + ") as item_count FROM adaptdev_datasource." + clientTableName + " Group By filter_value Order By item_count Desc Limit @limit", args);

                return data;
            }
        }

        public void SaveColumnConfigs(string clientId, ClientColumnConfig[] clientColumnConfigs)
        {
            //drop existing and then add the new items
            using (var conn = GetConnection())
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    //deletes first
                    DynamicParameters args = new DynamicParameters();
                    args.Add("Id", clientId);
                    var data = conn.Execute("DELETE FROM adaptdev_datasource.client_column_configurations WHERE client_id=@Id", args);

                    foreach (ClientColumnConfig c in clientColumnConfigs)
                    {
                        
                        DynamicParameters moreargs = new DynamicParameters();
                        moreargs.Add("clientid", c.ClientId);
                        moreargs.Add("DBColumnName", c.DBColumnName);
                        moreargs.Add("OrderIndex", c.OrderIndex);
                        moreargs.Add("DisplayName", c.DisplayName);
                        moreargs.Add("FormatString", c.FormatString);
                        moreargs.Add("IsDisplayed", c.IsDisplayed);
                        moreargs.Add("IsFilter", c.IsFilter);
                        conn.Execute("INSERT INTO adaptdev_datasource.client_column_configurations(client_id, db_column_name, order_index, display_name, format_string, is_displayed, is_filter) values (@clientid, @dbcolumnname, @orderindex, @displayname, @formatstring, @isdisplayed, @isfilter);", moreargs);
                    }

                    //commit
                    transaction.Commit();
                }

            }


        }
    }
}