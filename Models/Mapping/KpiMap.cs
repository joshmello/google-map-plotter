using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper.FluentMap.Mapping;
using FSGEMappingTool.Models.Domain;

namespace FSGEMappingTool.Models.Mapping
{
    public class KpiMap : EntityMap<Kpi>
    {
        public KpiMap()
        {
            Map(x => x.KpiId).ToColumn("kpi_id");
            Map(x => x.KpiName).ToColumn("kpi_name");
            Map(x => x.ValueColumn).ToColumn("kpi_value_column");
            Map(x => x.RankColumn).ToColumn("kpi_rank_column");
            Map(x => x.ValuePerAreaColumn).ToColumn("kpi_value_per_area_column");
            Map(x => x.RankPerAreaColumn).ToColumn("kpi_rank_per_area_column");
        }
    }
}