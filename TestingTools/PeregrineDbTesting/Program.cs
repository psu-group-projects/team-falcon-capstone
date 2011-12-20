using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.IO;

namespace PeregrineDbTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (string arg in args)
            {

                switch (arg)
                {
                    case "c":
                        createDB();
                        break;
                    default:
                        Console.WriteLine("Unknown argument: {0}", arg);
                        break;
                }
            }
        }

        static void createDB()
        {
            FileInfo file;
            string script;
            SqlCommand command;

            Console.WriteLine("Creating a new PeregrineDB will completely overwrite your current PeregrineDB (if it exists).");
            Console.WriteLine("Are you sure you wish to do this?");

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder["Data Source"] = "(local)";
            builder["integrated Security"] = true;
            Console.WriteLine(builder.ConnectionString);

            SqlConnection connection = new SqlConnection(builder.ConnectionString);

            connection.Open();

            file = new FileInfo("C:\\falcon\\source\\TestingTools\\PeregrineDbTesting\\SQL\\DropCreatePeregrineDB.sql");
            script = file.OpenText().ReadToEnd();
            command = new SqlCommand(script, connection);
            command.ExecuteNonQuery();

            connection.Close();
        }
    }
}
