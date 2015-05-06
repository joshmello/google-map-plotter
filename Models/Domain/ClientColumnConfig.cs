using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FSGEMappingTool.Models.Domain
{
    public class ClientColumnConfig
    {
        //I may not need the client ID - maybe just map to Dataset Config?
        //will need to distinguish new vs existing in edit view
        public string ClientId { get; set; }
        public int OrderIndex { get; set; }
        public string DBColumnName { get; set; }
        public string DisplayName { get; set; }
        public string FormatString { get; set; }
        public bool IsDisplayed { get; set; }
        public bool IsFilter { get; set; }
    }
}