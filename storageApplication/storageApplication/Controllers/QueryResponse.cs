using System;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;

namespace storageApplication.Controllers
{
	public class QueryResponse
	{
		public QueryResponse()
		{
		}

		/// <summary>
		/// return status 
		/// </summary>
		public bool isSucess { get; set; } = true;

		/// <summary>
		/// error message 
		/// </summary>
		public string message { get; set; }

		/// <summary>
		/// return data
		/// </summary>
        public JsonNode data { get; set; }

    }
}

