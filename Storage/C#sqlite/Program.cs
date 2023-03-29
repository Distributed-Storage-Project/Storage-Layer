﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Kusto.Language;
using Kusto.Language.Symbols;
using Kusto.Language.Syntax;
namespace Csharpsqlite
{
    class Program

    {
        static void Main(string[] args)
        {

            string createQuery = @"CREATE TABLE IF NOT EXISTS
                                  [storage] (
                                  [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                  [Timestamp] DATETIME DEFAULT CURRENT_TIMESTAMP,
                                  [Level] NVARCHAR(2048) NULL,
                                  [Service] NVARCHAR(2048) NULL
                                  )";



            //database file
            System.Data.SQLite.SQLiteConnection.CreateFile("storage.db");
            using (System.Data.SQLite.SQLiteConnection conn = new System.Data.SQLite.SQLiteConnection("data source=storage.db"))
            {
                using (System.Data.SQLite.SQLiteCommand cmd = new System.Data.SQLite.SQLiteCommand(conn))
                {
                    conn.Open();
                    //Console.WriteLine("Test 2");
                    cmd.CommandText = createQuery;
                    //Console.WriteLine("Test 3");
                    cmd.ExecuteNonQuery();
                    // Console.WriteLine("Test 4"); 
                    cmd.CommandText = "INSERT INTO storage(Level,Service) values('e' , 'Inferences.UnusualEvents_Main')";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "INSERT INTO storage(Level,Service) values('a' , 'Inferences.UnusualEvents_Main')";
                    cmd.ExecuteNonQuery();


                    //query
                    cmd.CommandText = "SELECT * from storage";
                    using (System.Data.SQLite.SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["Level"] + ":" + reader["Service"]);
                        }
                        conn.Close();

                    }

                }
            }
            Console.ReadLine();


            string KQLquery = "T | project a = a + b | where a > 10.0";
            KustoCode code = KustoCode.Parse(KQLquery);
            Console.WriteLine(code.Syntax.ToString());
            Console.WriteLine("Test 2");


            // convert Kusto code to C# code
            // string cSharpCode = code.ToCSharp();

            // build SQL query from C# code
            //string sqlQuery = $"SELECT * FROM MyTable WHERE {cSharpCode}";

            // execute SQL query using ADO.NET
            //string connectionString = "Data Source=MyServer;Initial Catalog=MyDatabase;Integrated Security=True;";
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //  connection.Open();

            //using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            //{
            // SqlDataAdapter adapter = new SqlDataAdapter(command);
            // DataTable table = new DataTable();
            //adapter.Fill(table);

            // foreach (DataRow row in table.Rows)
            //{
            //    Console.WriteLine(row["MyColumn"]);
            //}
            // }
            //}

        }


    }

}
