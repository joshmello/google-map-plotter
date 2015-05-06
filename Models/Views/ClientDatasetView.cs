using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using FSGEMappingTool.Models.Domain;

namespace FSGEMappingTool.Models.Views
{
    public class ClientDatasetView
    {
        public IEnumerable<DBColumn> DBColumnNames { get; set; }
        public IEnumerable<dynamic> DataRows { get; set; } 
        
    }
}