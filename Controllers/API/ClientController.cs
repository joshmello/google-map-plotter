using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FSGEMappingTool.Models.Domain;
using FSGEMappingTool.Models.Repositories;

namespace FSGEMappingTool.Controllers.API
{
    public class ClientController : ApiController
    {

        private ClientRepository _clientRepository = new ClientRepository();
        private DatasetRepository _datasetRepository = new DatasetRepository();

        // GET api/client/5
        [HttpGet]
        public Client Details(string id)
        {
            return _clientRepository.GetOne(id);
        }

        
        // GET api/client/5
        [HttpGet]
        public ClientDatasetConfig DatasetConfig(string id)
        {
            ClientDatasetConfig dsc = new ClientDatasetConfig();
            dsc.ColumnConfigs = _datasetRepository.GetClientColumnConfigs(id);
            return dsc;

        }


    }
}
