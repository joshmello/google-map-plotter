using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dapper.FluentMap;
using FSGEMappingTool.Models.Domain;
using FSGEMappingTool.Models.Mapping;

namespace FSGEMappingTool.App_Start
{
    public class MapConfig
    {

        public static void InitializeDataMaps()
        {
            FluentMapper.Intialize(config =>
            {
                config.AddMap(new ClientMap());
                config.AddMap(new DBColumnMap());
                config.AddMap(new ClientColumnConfigMap());
                config.AddMap(new KpiMap());
                config.AddMap(new DataFilterValueMap());
            });
        }
    }
}