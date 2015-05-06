using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using Dapper.Fluent;
using FSGEMappingTool.Models.Domain;


namespace FSGEMappingTool.Models.Repositories
{
    public class ClientRepository : RepositoryBase
    {
        public ClientRepository()
        {
            
        }

        
        public IList<Client> GetAll()
        {

            using (var conn = GetConnection())
            {
                conn.Open();
                var clients = conn.Query<Client>("select * from adaptdev_datasource.clients");
                return clients.ToList();
            }
        }

        public Client GetOne(string clientId)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                DynamicParameters args = new DynamicParameters();
                args.Add("clientId", clientId);
                var clients = conn.Query<Client>("select * from adaptdev_datasource.clients where client_id=@clientId", args);
                return clients.First();
            }
        }

        

        //get columns from dataset
        //SELECT column_name FROM information_schema.columns WHERE table_name ='table';

        public IList<Client> GetTestClients()
        {
            IList<Client> clientList = new List<Client>();

            Client c = new Client { ClientId = "test1", DBTableName = "Test1_Data", DisplayName = "Test Client 1" };
            clientList.Add(c);
            c = new Client { ClientId = "test2", DBTableName = "Test1_Data", DisplayName = "Test Client 2" };
            clientList.Add(c);
            c = new Client { ClientId = "test3", DBTableName = "Test1_Data", DisplayName = "Test Client 3" };
            clientList.Add(c);
            c = new Client { ClientId = "test4", DBTableName = "Test1_Data", DisplayName = "Test Client 4" };
            clientList.Add(c);
            return clientList;
        } 
    }
}