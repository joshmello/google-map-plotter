using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper.FluentMap.Mapping;
using FSGEMappingTool.Models.Domain;

namespace FSGEMappingTool.Models.Mapping
{
    public class DBColumnMap : EntityMap<DBColumn>
    {
        public DBColumnMap()
        {
            Map(x => x.ColumnName).ToColumn("column_name");
        }
    }
}