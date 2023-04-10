using System;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using storageApplication.Repository;

namespace storageApplication.Controllers
{
	public class QueryResponse
	{
		public QueryResponse()
		{
		}

        public QueryResponse(bool isSucess, string message)
        {
			this.isSucess = isSucess;
			this.message = message;
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
        public List<List<string>> data { get; set; }

    }
}

