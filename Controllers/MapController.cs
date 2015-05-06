using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FSGEMappingTool.Models.Domain;
using FSGEMappingTool.Models.Repositories;
using FSGEMappingTool.Models.Views;

namespace FSGEMappingTool.Controllers
{
    public class MapController : Controller
    {
        

        private ClientRepository _clientRepository = new ClientRepository();
        private DatasetRepository _datasetRepository = new DatasetRepository();
        private KpiRepository _kpiRepository = new KpiRepository();


        //this view will be the non-iframed version (should just iFrame in the iframe view for consistency)
        public ActionResult Preview(string id)
        {
            ViewBag.clientId = id;   
            return View();
        }

        public ActionResult Iframe(string id)
        {
            ClientMapView view = new ClientMapView();
            view.ClientId = id;
            if (Request.QueryString["filter"] != null)
            {
                view.SelectedFilter = Request.QueryString["filter"];
            }

            
            Client client = _clientRepository.GetOne(id);
            if (client != null)
            {
                
                //get dataset - this is where you can get the display columns and the filter columns
                ClientDatasetConfig c = new ClientDatasetConfig();
                var configs = _datasetRepository.GetClientColumnConfigs(client.ClientId);
                c.ColumnConfigs = configs.ToList();
                view.DatasetConfig = c;
                //once you set the columns configs, you have access to the filter and dsiplay configs

                if (view.SelectedFilter == "")
                {
                    var filter = view.DatasetConfig.FilterConfigs.First();
                    view.SelectedFilter = filter.DBColumnName;
                } 

                //now get the top filter values for selected filter
                view.TopFilterValues = _datasetRepository.GetTopFilterValues(client.DBTableName, view.SelectedFilter, "9");
                

                //now get the datarows
                var data = _datasetRepository.GetClientDataset(client.DBTableName);
                
                //until we do filtering on the client side, let's just filter the dataset here
                //var filteredData = from x in data
                //                   where x[view.SelectedFilter] != "client_id"
                //                   select x;


                view.DataRows = data;



            }

            return View(view);
        }





        public ActionResult IframeKpi(string id)
        {
            ClientMapKpiView view = new ClientMapKpiView();
            view.ClientId = id;
            if (Request.QueryString["kpi"] != null)
            {
                view.SelectedKpiId = Request.QueryString["kpi"];
            }
            
            if (Request.QueryString["mode"] != null)
            {
                view.KPIViewMode = Request.QueryString["mode"];
            }

            Client client = _clientRepository.GetOne(id);
            if (client != null)
            {
                //we need them all to bind the Select list
                var kpis = _kpiRepository.GetAll();
                view.Kpis = kpis;
                Kpi selectedKpi;
                //but now we need the individual values to help bind in the UI
                if (view.SelectedKpiId == "")
                {
                    //get the first one by default
                    selectedKpi = kpis[0];
                    view.SelectedKpiId = selectedKpi.KpiId.ToString();
                }
                else
                {
                    selectedKpi = kpis.First(s => s.KpiId.ToString() == view.SelectedKpiId);
                }

                view.SelectedKpiName = selectedKpi.KpiName;

                //set values from selected KPI
                if (view.KPIViewMode == "normalized")
                {
                    view.SelectedKpiRankColumn = selectedKpi.RankPerAreaColumn;
                    view.SelectedKpiDataColumn = selectedKpi.ValuePerAreaColumn;
                }
                else
                {
                    view.SelectedKpiRankColumn = selectedKpi.RankColumn;
                    view.SelectedKpiDataColumn = selectedKpi.ValueColumn;
                }

                if (!string.IsNullOrEmpty(view.SelectedKpiRankColumn))
                {
                    var list = _kpiRepository.GetTopAndBottomKpisForClientByKPI(client.ClientId, client.DBTableName, view.SelectedKpiRankColumn,"10");
                    view.ClientKpis = list;
                }

            }

            return View(view);
        }


        
    }
}
