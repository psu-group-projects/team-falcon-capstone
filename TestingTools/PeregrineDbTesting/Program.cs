using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
// using System.IO; // needed for accessing files - not currently needed
using System.Text.RegularExpressions;

namespace PeregrineDBTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() == 0)
            {
                Console.WriteLine("Arguments are required. Here are your options...");
                Console.WriteLine("create - create a fresh Peregrine database");
            }
            else foreach (string arg in args)
            {

                switch (arg)
                {
                    case "create":
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
            string script;
            string reply;
            string oldDBName = "PeregrineDB";
            string dbName = "TestDB";

            Console.Write("Enter a database name (Enter for {0}): ", dbName);
            reply = Console.ReadLine();
            if (reply != "" && reply != "master") dbName = reply;            

            Console.WriteLine("Warning! Creating a new {0} database will completely overwrite any other database of the same name.", dbName);
            Console.Write("Are you sure you wish to do this (yes/no)? ");
            reply = Console.ReadLine();
            if (reply.ToLower() == "yes")
            {

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder["Data Source"] = "(local)";
                builder["integrated Security"] = true;

                Console.WriteLine("Connecting to SQL server using \"{0}\" connection string...", builder.ConnectionString);

                SqlConnection connection = new SqlConnection(builder.ConnectionString);

                connection.Open();

                Console.WriteLine("Creating {0} database...", dbName);

                script = Properties.Resources.DropCreatePeregrineDB_sql + Environment.NewLine;
                script = script.Replace(oldDBName, dbName);
                executeSqlStrings(connection, script);

                Console.WriteLine("Creating tables...");

                script = Properties.Resources.DropCreateProcessTable_sql + Environment.NewLine;
                script = script.Replace(oldDBName, dbName);
                executeSqlStrings(connection, script);

                script = Properties.Resources.DropCreateJobTable_sql + Environment.NewLine;
                script = script.Replace(oldDBName, dbName);
                executeSqlStrings(connection, script);

                script = Properties.Resources.DropCreateMessagesTable_sql + Environment.NewLine;
                script = script.Replace(oldDBName, dbName);
                executeSqlStrings(connection, script);

                script = Properties.Resources.DropCreateLogRelTable_sql + Environment.NewLine;
                script = script.Replace(oldDBName, dbName);
                executeSqlStrings(connection, script);

                script = Properties.Resources.DropCreateSysdiagramsTable_sql + Environment.NewLine;
                script = script.Replace(oldDBName, dbName);
                executeSqlStrings(connection, script);

                Console.WriteLine("Closing connection to SQL server...");

                connection.Close();

                Console.WriteLine("Finished!");
            }
        }

        // Commands can only be one batch. Batches are seperated by GO statements.
        static string[] splitSqlAtGo(string commandString)
        {
            string delimiter = "GO" + Environment.NewLine;
            string[] commands = Regex.Split(commandString, delimiter);
            return commands;
        }

        static void executeSqlStrings(SqlConnection connection, string script)
        {
            string[] commandtexts = splitSqlAtGo(script);

            foreach (string commandtext in commandtexts)
            {
                if (commandtext != "")
                {
                    //Console.WriteLine("Splt--------------");
                    //Console.WriteLine(commandtext);
                    SqlCommand command = new SqlCommand(commandtext, connection);
                    command.ExecuteNonQuery();
                }
            }
        }

    }
}
