using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FSGEMappingTool.Models.Domain
{
    public class Client
    {
        public string ClientId { get; set; }
        public string DisplayName { get; set; }
        public string DBTableName { get; set; }
        public bool DataRefreshNeeded { get; set; }
        public string SpreadsheetLocation { get; set; }
        public DateTime DataLastUpdated { get; set; }
    }
}