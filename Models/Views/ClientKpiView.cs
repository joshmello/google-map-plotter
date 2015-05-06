using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FSGEMappingTool.Models.Domain;

namespace FSGEMappingTool.Models.Views
{
    public class ClientKpiView
    {
        public string SelectedKpiRank = "";
        public string RecordLimit = "10"; //default to 10 of each top and bottom records
        public string SelectedSite {get; set; }
        public bool ShowSearchMessage = true;
        public IEnumerable<Kpi> Kpis { get; set; }
        public IEnumerable<dynamic> ClientKpis { get; set; }
    }
}