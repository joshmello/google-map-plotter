using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FSGEMappingTool.Models.Domain
{
    public class ClientDatasetConfig
    {
        private IEnumerable<ClientColumnConfig> _columnConfigs = new List<ClientColumnConfig>();

        public IEnumerable<ClientColumnConfig> ColumnConfigs
        {
            get { return _columnConfigs; }
            set { _columnConfigs = value; }
        }

        public IEnumerable<ClientColumnConfig> FilterConfigs
        {
            get
            {
                var items =
                    from x in _columnConfigs
                    where x.IsFilter == true
                    select x; 
                return items;
            }
        }
        
        public IEnumerable<ClientColumnConfig> DisplayConfigs
        {
            get
            {
                var items =
                  from x in _columnConfigs
                  where x.IsDisplayed == true
                  select x;
                return items;
            }
        }

        //public string[] DisplayFieldNames
        //{
        //    get
        //    {
        //        var items =
        //          from x in _columnConfigs
        //          where x.IsDisplayed == true
        //          select x.DisplayName;
        //        return items.ToArray();
        //    }
        //}
    }
}