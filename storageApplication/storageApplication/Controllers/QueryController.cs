using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using storageApplication.Repository;

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
        public QueryResponse Query([FromBody] QueryRequest request)
        {
            string? query = request.query;
            if (string.IsNullOrEmpty(query) || (!query.ToLower().StartsWith("select") && !query.ToLower().StartsWith("insert into") && !query.ToLower().StartsWith("delete")) && !query.ToLower().StartsWith("create table") && !query.ToLower().StartsWith("drop table")) {
                return new QueryResponse(false, "Query is invalid!");
            }
            QueryResponse response = new QueryResponse();
            try
            {
                List<List<string>> data = _repository.query(query);
                response.data = data;
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
