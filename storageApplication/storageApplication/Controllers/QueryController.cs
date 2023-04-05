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
            if (string.IsNullOrEmpty(query) || !query.ToLower().StartsWith("select")) {
                return new QueryResponse(false, "Query is invalid!");
            }
            QueryResponse response = new QueryResponse();
            try
            {
                _repository.query(query);

                // todo
            }
            catch (Exception e)
            {
                response.isSucess = false;
                response.message = e.Message;
                Console.WriteLine(e.StackTrace);
            }

            return response;
        }
        //jay below
        public void query(string query)
        {
            List<List<string>> results = new List<List<string>>();

            using (SqliteConnection conn = new SqliteConnection("Data Source=temp335.db"))
            {
                using (SqliteCommand command = new SqliteCommand())
                {
                    try
                    {
                        command.Connection = conn;
                        conn.Open();
                        command.CommandText = query;
                        SqliteDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            List<string> row = new List<string>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Type type = reader.GetFieldType(i);
                                string key = reader.GetName(i);
                                string? value = reader[i] == null ? "" : reader[i].ToString();
                                value = String.Format(value, type);
                                row.Add(value);
                            }
                            results.Add(row);
                        }
                    }
                    finally
                    {
                        conn.Close();
                    }

                }
            }

            QueryResponse response = new QueryResponse();
            response.data = results;
        }


    }
}
