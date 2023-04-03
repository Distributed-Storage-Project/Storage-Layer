using System;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;

namespace Models
{
	public interface IRepository
	{
        bool insert(String query);

        JsonNode query(String query);
    }
}

