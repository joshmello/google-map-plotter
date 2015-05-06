using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using FSGEMappingTool.Models.Domain;
using Newtonsoft;
using Newtonsoft.Json;

namespace FSGEMappingTool.Models.Views
{
    public class ClientMapView
    {
        public string ClientId { get; set; }

        //this will have filters and display fields
        public ClientDatasetConfig DatasetConfig { get; set; }

        public ClientColumnConfig[] FilterConfigs()
        {
            var items = DatasetConfig.FilterConfigs;
            if (items != null)
            {
                return items.ToArray();
            }
            return new ClientColumnConfig[0];
        }

        public ClientColumnConfig[] DisplayConfigs()
        {
            var items = DatasetConfig.DisplayConfigs;
            if (items != null)
            {
                return items.ToArray();
            }
            return new ClientColumnConfig[0];
        }

        public IEnumerable<dynamic> DataRows { get; set; }
        public IEnumerable<dynamic> TopFilterValues = new List<dynamic>();
        public string SelectedFilter = "";

        public string[] Icons = new[] { "markermed_blue.png", "markermed_green.png", "markermed_purple.png", "markermed_red.png", "markermed_orange.png", "markermed_pink.png", "markermed_grey.png", "markermed_black.png", "markermed_blue.png", "markermed.png" };

        public string GetDatasetAsJson()
        {
            string retval = "";

            

            //retval = 

            return retval;
        }
    }
}