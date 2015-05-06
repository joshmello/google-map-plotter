using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using FSGEMappingTool.Models.Domain;
using FSGEMappingTool.Models.Repositories;
using FSGEMappingTool.Models.Views;


namespace FSGEMappingTool.Controllers
{
    public class ClientController : Controller
    {

        private ClientRepository _clientRepository = new ClientRepository();
        private DatasetRepository _datasetRepository = new DatasetRepository();
        private KpiRepository _kpiRepository = new KpiRepository();
        

        public ActionResult Index()
        {
            
            return View(_clientRepository.GetAll());
        }

        //
        // GET: /Client/Details/5

        public ActionResult Details(string id)
        {
            return View(_clientRepository.GetOne(id));
        }

        public ActionResult Dataset(string id)
        {
            ClientDatasetView view = new ClientDatasetView();
            
            Client client = _clientRepository.GetOne(id);
            if (client != null)
            {
                var columns = _datasetRepository.GetColumnsForClientDBTable(client.DBTableName);
                view.DBColumnNames = columns;
                var data = _datasetRepository.GetClientDataset(client.DBTableName, "150");
                view.DataRows = data;
            }
            return View(view);
        }

        public ActionResult DatasetConfig(string id)
        {
            ClientDatasetConfigEdit view = new ClientDatasetConfigEdit();

            Client client = _clientRepository.GetOne(id);
            if (client != null)
            {
                var columns = _datasetRepository.GetColumnsForClientDBTable(client.DBTableName);
                view.DBColumnNames = columns.ToList();
                var data = _datasetRepository.GetClientColumnConfigs(client.ClientId);
                view.ColumnConfigs = data.ToList();
            }

            return View(view);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult DatasetConfig(string id, FormCollection collection)
        {
            
            //loop through each field to see if there are any with display or filter checked - ignore all others
            IList<ClientColumnConfig> configs = new List<ClientColumnConfig>();
            foreach (var key in collection.AllKeys)
            {
                //make sure to only loop through once per row (use column name field)
                if (key.ToLower().StartsWith("column_")) {
                    //parse our db col name and then build a new config object using all values in the row
                    string dbcolname = key.Substring(key.IndexOf(Convert.ToChar("_")) + 1);

                    //now only create items for fields that are displayed or filters
                    if (collection["isdisplay_" + dbcolname] != null || collection["isfilter_" + dbcolname] != null)
                    {
                        
                        ClientColumnConfig ccc = new ClientColumnConfig();
                        ccc.DBColumnName = dbcolname;
                        ccc.ClientId = id;
                        ccc.DisplayName = collection["display_" + dbcolname];
                        ccc.FormatString = collection["format_" + dbcolname];

                        if (collection["isdisplay_" + dbcolname] != null)
                        {
                            if (collection["isdisplay_" + dbcolname] == "on")
                            {
                                ccc.IsDisplayed = true;
                            }
                        }

                        if (collection["isfilter_" + dbcolname] != null)
                        {
                            if (collection["isfilter_" + dbcolname] == "on")
                            {
                                ccc.IsFilter = true;
                            }
                        }
                    
                        if (collection["idx_" + dbcolname] != null)
                        {
                            try
                            {
                                ccc.OrderIndex = Convert.ToInt32(collection["idx_" + dbcolname]);
                            }
                            catch (Exception)
                            {
                                ccc.OrderIndex = 99;
                            }
                        }
                    
                        //add new config to the list 
                        configs.Add(ccc);
                    }
                    else
                    {
                        //ignore the rows where field is not selected as either filter or display
                    }

                }
                
            }
            //ok
            int clen = configs.Count(); //this line was for debugging

            //now drop all existing configs and replace them with the new collection
            _datasetRepository.SaveColumnConfigs(id, configs.ToArray());


            return RedirectToAction("DatasetConfig", new {id = id});
        }

        //GET
        public ActionResult Kpis(string id)
        {
            ClientKpiView view = new ClientKpiView();

            if (Request.QueryString["kpirank"] != null)
            {
                view.SelectedKpiRank = Request.QueryString["kpirank"];
            }
            if (Request.QueryString["limit"] != null)
            {
                view.RecordLimit = Request.QueryString["limit"];
            }
            if (Request.QueryString["site"] != null)
            {
                view.SelectedSite = Request.QueryString["site"];
            }

            Client client = _clientRepository.GetOne(id);
            if (client != null)
            {
                //get main KPI list
                var kpis = _kpiRepository.GetAll();
                view.Kpis = kpis;
                //set default view
                if (string.IsNullOrEmpty(view.SelectedKpiRank))
                {
                    view.SelectedKpiRank = kpis.First().RankPerAreaColumn;
                }

                if (!String.IsNullOrEmpty(view.SelectedSite))
                {
                    //search for site ID overrides the kpi specific search
                    view.ShowSearchMessage = false;
                    var list = _kpiRepository.GetKpisForClientSite(client.ClientId, view.SelectedSite);
                    view.ClientKpis = list;
                }
                else if (!string.IsNullOrEmpty(view.SelectedKpiRank))
                {
                    view.ShowSearchMessage = false;
                    var list = _kpiRepository.GetTopAndBottomKpisForClientByKPI(client.ClientId, client.DBTableName, view.SelectedKpiRank,
                        view.RecordLimit);
                    view.ClientKpis = list;
                }
                else
                {
                    //don't even do a query - let's see if it bombs our view
                }
                
            }
            return View(view);
        }

        //
        // GET: /Client/Create

        //public ActionResult Create()
        //{
        //    return View();
        //}

        //
        // POST: /Client/Create

        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //
        // GET: /Client/Edit/5

        public ActionResult Edit(string id)
        {
            return View(_clientRepository.GetOne(id));
        }

        //
        // POST: /Client/Edit/5

        [HttpPost]
        public ActionResult Edit(string id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Details");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Client/Delete/5

        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //
        // POST: /Client/Delete/5

        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
