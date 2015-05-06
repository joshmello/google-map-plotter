using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using Dapper;
using Dapper.FluentMap;
using Dapper.FluentMap.Mapping;
using FSGEMappingTool.Models.Domain;

namespace FSGEMappingTool.Models.Mapping
{
    public class ClientMap : EntityMap<Client>
    {
        public ClientMap()
        {
            Map(x => x.ClientId).ToColumn("client_id");
            Map(x => x.DisplayName).ToColumn("display_name");
            Map(x => x.DBTableName).ToColumn("db_table_name");
            Map(x => x.DataRefreshNeeded).ToColumn("data_refresh_needed");
            Map(x => x.SpreadsheetLocation).ToColumn("spreadsheet_location");
            Map(x => x.DataLastUpdated).ToColumn("data_last_updated");
        }
    }
}