using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using FSGEMappingTool.Models.Domain;

namespace FSGEMappingTool.Models.Views
{
    public class ClientMapKpiView
    {
        public string ClientId = "";
        public string KPIViewMode = "";
        
        public string SelectedKpiId = "";
        public string SelectedKpiRankColumn = "";
        public string SelectedKpiDataColumn = "";
        public string SelectedKpiName = "";
        
        public string RecordLimit = "10"; //default to 10 of each top and bottom records
        
        public IEnumerable<Kpi> Kpis { get; set; }
        public IEnumerable<dynamic> ClientKpis { get; set; }

        
        
    }
}