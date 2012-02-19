using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Microsoft.SqlServer.Management.Smo;       // Requires SqlServer Assemblies
using Microsoft.SqlServer.Management.Common;    // Requires SqlServer Assemblies

namespace PeregrineCreateDB
{
    class Program
    {
        static void Main(string[] args)
        {
            createDB();
        }

        static void createDB()
        {
            string dbName = "PeregrineTestDB";  // name of db to be created
            string reply;                       // for user input
            Boolean okayToGo;                   // for input loop
            Boolean createDB            = false;    // Create DB on the local server
            Boolean createCleanupJob    = false;    // Create a Scheduled job on local
                                                    // server for database cleanup
            
            okayToGo = false;
            while (okayToGo == false)
            {
                Console.Write("Enter a database name (Enter for {0}): ", dbName);
                reply = Console.ReadLine();
                // make sure user doesn't enter 'master' and give extra warning
                // for name of our master PeregrineDB
                if (reply == "master") Console.WriteLine("The database cannot be named master.");
                else
                {
                    if (reply != "") dbName = reply;
                    if (String.Compare(dbName, Properties.Resources.OldDatabaseName, true) == 0)
                        Console.WriteLine(Environment.NewLine + "Warning! {0} is the name of the master database. If you are executing this on the capstone lab server, DO NOT CONTINUE. The master database would be overwritten!", Properties.Resources.OldDatabaseName);
                    okayToGo = true;
                }
            }

            Console.WriteLine(Environment.NewLine + "Warning! Creating a new {0} database will completely overwrite any other database of the same name." + Environment.NewLine, dbName);

            Console.Write("Are you sure you wish drop and create a {0} database? (yes/no) ", dbName);
            reply = Console.ReadLine();
            if (reply.ToLower() == "yes") createDB = true;

            Console.Write("Would you like to install a scheduled job for {0} database cleanup? (yes/no) ", dbName);
            reply = Console.ReadLine();
            if (reply.ToLower() == "yes") createCleanupJob = true;


            // create connection string
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder["Data Source"] = "(local)";
            builder["integrated Security"] = true;

            // connect to SQL server
            Console.WriteLine(Environment.NewLine + "Connecting to SQL server using \"{0}\" connection string...", builder.ConnectionString);
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            connection.Open();
            Server server = new Server(new ServerConnection(connection));

            // drop and create Peregrine database
            if (createDB == true)
            {
                Console.WriteLine("Dropping old and creating empty {0} database...", dbName);
                executeScript(server, dbName, Properties.Resources.DropCreateEmptyDatabaseSql);

                Console.WriteLine("Creating {0} database tables, views, and stored procedures...", dbName);
                executeScript(server, dbName, Properties.Resources.CreateDatabaseSql);
            }

            // drop and create Peregrine database
            if (createCleanupJob == true)
            {
                Console.Write("Installing scheduled job for {0} database cleanup...", dbName);
                executeScript(server, dbName, Properties.Resources.DropCreateCleanupJob);
            }

            // Close database connection
            Console.WriteLine("Closing connection to SQL server...");
            connection.Close();

            Console.WriteLine("Finished!");

        }

        static void executeScript(Server server, string dbName, string script)
        {
            script = script + Environment.NewLine;
            script = script.Replace(Properties.Resources.OldDatabaseName, dbName);
            
            server.ConnectionContext.ExecuteNonQuery(script);
            //executeSqlStrings(connection, script);
        }

        // Commands can only be one batch. Batches are seperated by GO statements.
        [Obsolete]
        static string[] splitSqlAtGo(string commandString)
        {
            string delimiter = "GO" + Environment.NewLine;
            string[] commands = Regex.Split(commandString, delimiter);
            return commands;
        }

        [Obsolete]
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
