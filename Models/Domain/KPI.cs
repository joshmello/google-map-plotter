using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FSGEMappingTool.Models.Domain
{
    public class Kpi
    {
        public int KpiId { get; set; }
        public string KpiName { get; set; }
        public string ValueColumn { get; set; }
        public string RankColumn{ get; set; }
        public string ValuePerAreaColumn { get; set; }
        public string RankPerAreaColumn { get; set; }
    }
}