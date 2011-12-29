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
            // iterate through given arguments
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
            string dbName = "TestDB";           // name of db to be created
            string reply;                       // for user input

            Console.Write("Enter a database name (Enter for {0}): ", dbName);
            reply = Console.ReadLine();
            if (reply != "" && reply != "master") dbName = reply;            

            Console.WriteLine(Environment.NewLine + "Warning! Creating a new {0} database will completely overwrite any other database of the same name.", dbName);
            Console.Write("Are you sure you wish to do this (yes/no)? ");
            reply = Console.ReadLine();
            if (reply.ToLower() == "yes")
            {
                // create connection string
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder["Data Source"] = "(local)";
                builder["integrated Security"] = true;

                Console.WriteLine(Environment.NewLine + "Connecting to SQL server using \"{0}\" connection string...", builder.ConnectionString);
                SqlConnection connection = new SqlConnection(builder.ConnectionString);
                connection.Open();

                Console.WriteLine("Creating {0} database...", dbName);
                executeScript(connection, dbName, Properties.Resources.DropCreatePeregrineDB_sql);

                Console.WriteLine("Creating tables...");
                executeScript(connection, dbName, Properties.Resources.DropCreateProcessTable_sql);
                executeScript(connection, dbName, Properties.Resources.DropCreateJobTable_sql);
                executeScript(connection, dbName, Properties.Resources.DropCreateMessagesTable_sql);
                executeScript(connection, dbName, Properties.Resources.DropCreateLogRelTable_sql);
                // I'm not sure the following table is needed.
                executeScript(connection, dbName, Properties.Resources.DropCreateSysdiagramsTable_sql);

                Console.WriteLine("Creating stored procedures...");
                executeScript(connection, dbName, Properties.Resources.CreateSP_ShowProcesses_sql);

                Console.WriteLine("Closing connection to SQL server...");
                connection.Close();

                Console.WriteLine("Finished!");
            }
        }

        static void executeScript(SqlConnection connection, string dbName, string script)
        {
            string oldDBName = "PeregrineDB";   // used for search and replace in scripts

            script = script + Environment.NewLine;
            script = script.Replace(oldDBName, dbName);
            executeSqlStrings(connection, script);
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
