using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections;
using Models;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;

namespace storageApplication.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class QueryController : ControllerBase
    {
        protected readonly IRepository _repository;

        public QueryController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpPost(Name = "query")]
        public QueryResponse Query(String query)
        {
            QueryResponse response = new QueryResponse();
            // check query type by prefix
            string tempCmd = query.ToLower();
            bool isQuery = tempCmd.StartsWith("select");
            try
            {
                if (isQuery)
                {
                    JsonNode array = _repository.query(query);
                    response.data = array;
                }
                else
                {
                    bool res = _repository.insert(query);
                    response.isSucess = res;
                }
            }
            catch (Exception e)
            {
                response.isSucess = false;
                response.message = e.Message;
                Console.WriteLine(e.StackTrace);
            }

            return response;
        }

    }
}
