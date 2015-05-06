using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using FSGEMappingTool.Models.Domain;

namespace FSGEMappingTool.Models.Views
{
    public class ClientDatasetConfigEdit
    {
        
        private IList<DBColumn> _columns = new List<DBColumn>(); 
        private IList<ClientColumnConfig> _configs = new List<ClientColumnConfig>(); 
        
        public IList<DBColumn> DBColumnNames {
            get { return _columns;  }
            set { _columns = value; }
        }

        public IList<ClientColumnConfig> ColumnConfigs {
            get { return _configs; }
            set { _configs = value; }
        }

        public IList<DBColumn> GetUnconfiguredColumns()
        {
            IList<DBColumn> filteredColumns = new List<DBColumn>();
            foreach (DBColumn col in DBColumnNames)
            {
                //loop through all columns names and create a list of items that don't appear in 
                if (!ColumnConfigs.Any(item => item.DBColumnName == col.ColumnName))
                {
                    filteredColumns.Add(col);
                }                
            }
            return filteredColumns;
        }

    }
}