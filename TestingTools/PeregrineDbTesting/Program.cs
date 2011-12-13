using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;

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
                        Console.WriteLine("Creating a new PeregrineDB will completely overwrite your current PeregrineDB.");
                        Console.WriteLine("Are you sure you wish to do this?");

                        SqlConnection connection = new SqlConnection("connectionString");
                        SqlCommand command = new SqlCommand("script",connection);
                        command.ExecuteNonQuery();

                        break;
                    default:
                        Console.WriteLine("Unknown argument: {0}", arg);
                        break;
                }
            }
        }
    }
}
