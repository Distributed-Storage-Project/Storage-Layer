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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace storageApplication.Repository
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
                }
            }
        }

        public void query(string query)
        {
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
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Type type = reader.GetFieldType(i);
                                string key = reader.GetName(i);
                                string? value = reader[i] == null ? "" : reader[i].ToString();
                                value = String.Format(value, type);
                                // todo
                            }
                        }
                    }
                    finally
                    {
                        conn.Close();
                    }

                }
            }
        }
    }
}

