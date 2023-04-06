using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;

namespace storageApplication.Repository
{
	public interface IRepository
	{
        bool insert(String query);

        List<List<string>> query(String query);
    }
}

