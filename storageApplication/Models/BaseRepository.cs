using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Repository
{
	public class BaseRepository : IRepository
	{

        public bool insert(string insertQuery)
        {
            using (SqliteConnection conn = new SqliteConnection("Data Source=temp335.db"))
            {
                using (SqliteCommand command = new SqliteCommand())
                {
                    try
                    {
                        command.Connection = conn;
                        conn.Open();
                        command.CommandText = insertQuery;
                        command.ExecuteNonQuery();
                        return true;
                    }
                    finally
                    {
                        conn.Close();
                    }
                    return false;
                }
            }
        }

        public JsonNode query(string query)
        {
            using (SqliteConnection conn = new SqliteConnection("Data Source=temp335.db"))
            {
                using (SqliteCommand command = new SqliteCommand())
                {
                    // assemble result as a json string
                    StringBuilder jsonString = new StringBuilder();
                    jsonString.Append("[");
                    try
                    {
                        command.Connection = conn;
                        conn.Open();
                        command.CommandText = query;
                        SqliteDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            jsonString.Append("{");
                            for (int i = 0; i < reader.FieldCount; i++) {
                                Type type = reader.GetFieldType(i);
                                string key = reader.GetName(i);
                                jsonString.Append("\"" + key + "\":");
                                string? value = reader[i] == null ? "" : reader[i].ToString();
                                value = String.Format(value, type);

                                // if type is string/datetime/int make value=""
                                if (type == typeof(string) || type == typeof(DateTime) || type == typeof(int)) {
                                    if (i < reader.FieldCount - 1)
                                    {
                                        jsonString.Append("\"" + value + "\",");
                                    }
                                    else
                                    {
                                        jsonString.Append(value);
                                    }
                                }
                                else
                                {
                                    if (i < reader.FieldCount - 1)
                                    {
                                        jsonString.Append("" + value + ",");
                                    }
                                    else
                                    {
                                        jsonString.Append(value);
                                    }
                                }

                            }
                            jsonString.Append("},");
                        }
                        // if json is empty format the jsonString or complete the json format
                        if (jsonString.Length == 1)
                        {
                            jsonString.Append("]");
                        }
                        else
                        {
                            jsonString.Remove(jsonString.Length - 3, 3);
                            jsonString.Append("}");
                            jsonString.Append("]");
                        }
                    }
                    finally
                    {
                        conn.Close();
                    }
                    return JsonObject.Parse(jsonString.ToString());
                }
            }
        }
    }
}

