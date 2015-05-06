using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper.FluentMap.Mapping;
using FSGEMappingTool.Models.Domain;

namespace FSGEMappingTool.Models.Mapping
{
    public class ClientColumnConfigMap : EntityMap<ClientColumnConfig>
    {
        public ClientColumnConfigMap()
        {
            Map(x => x.ClientId).ToColumn("client_id");
            Map(x => x.DBColumnName).ToColumn("db_column_name");
            Map(x => x.DisplayName).ToColumn("display_name");
            Map(x => x.FormatString).ToColumn("format_string");
            Map(x => x.IsDisplayed).ToColumn("is_displayed");
            Map(x => x.IsFilter).ToColumn("is_filter");
            Map(x => x.OrderIndex).ToColumn("order_index");
        }
    }
}