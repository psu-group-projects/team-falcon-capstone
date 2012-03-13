//------------------------------------------------------------------------------
// c. 2012 by Nicholas Benson and Devon Gleeson
// This is the console installer for the Peregrine Database
// Building this requires references to SQL Server 2008 library assemblies.
//
//-----------------------------------------------------------------------------


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
    /// <summary>
    /// Creates a Peregrine database with a given name on a local SQL server.
    /// Also will install a database maintenance job for given database.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            string dbName = "PeregrineTestDB";  // name of db to be created
            string reply;                       // for user input
            Boolean okayToGo;                   // for input loop
            Boolean createDB = true;            // Create DB on the local server
            Boolean createCleanupJob = true;    // Create a Scheduled job on local
                                                // server for database cleanup
            Boolean quietMode = false;          // No prompts. Use defaults.
                                                // Installs DB and cleanup job
            Boolean namePassedAsArg = false;    // Don't ask for database name if already
                                                // passed as an argument

            if (args.Length > 0)
            {
                for (int i = 0;  i < args.Length; i++)
                {
                    if (args[i] == "-q") quietMode = true;
                    else if (args[i] == "-d")       // install database only
                    {
                        createDB = true;
                        createCleanupJob = false;
                    }
                    else if (args[i] == "-c")       // install db cleanup only
                    {
                        createDB = false;
                        createCleanupJob = true;
                    }
                    else if (args[i] == "-n")       // set database name
                    {
                        i++;
                        dbName = args[i];
                        namePassedAsArg = true;
                    }
                }
            }

            if (quietMode != true)
            {
                if (namePassedAsArg == false)
                {
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
                            okayToGo = true;
                        }
                    }
                }
                if (String.Compare(dbName, Properties.Resources.OldDatabaseName, true) == 0)
                    Console.WriteLine(Environment.NewLine + "Warning! {0} is the name of the master database. If you are executing this on the capstone lab server, DO NOT CONTINUE. The master database would be overwritten!", Properties.Resources.OldDatabaseName);
                if (createDB == true)           // don't ask if turned off by commandline arg
                {
                    Console.WriteLine(Environment.NewLine + "Warning! Creating a new {0} database will completely overwrite any other database of the same name." + Environment.NewLine, dbName);
                    Console.Write("Are you sure you wish drop and create a {0} database? (yes/no) ", dbName);
                    reply = Console.ReadLine();
                    if (reply.ToLower() == "yes") createDB = true;
                    else createDB = false;
                }
                if (createCleanupJob == true)   // don't ask if turned off by commandline arg
                {
                    Console.Write("Would you like to install a scheduled job for {0} database cleanup? (yes/no) ", dbName);
                    reply = Console.ReadLine();
                    if (reply.ToLower() == "yes") createCleanupJob = true;
                    else createCleanupJob = false;
                }
            }

            // create connection string
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder["Data Source"] = "(local)";
            builder["integrated Security"] = true;

            // connect to SQL server
            if (quietMode == false) Console.WriteLine(Environment.NewLine + "Connecting to SQL server using \"{0}\" connection string...", builder.ConnectionString);
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            connection.Open();
            Server server = new Server(new ServerConnection(connection));

            // drop and create Peregrine database
            if (createDB == true)
            {
                if (quietMode == false) Console.WriteLine("Dropping old and creating empty {0} database...", dbName);
                executeScript(server, dbName, Properties.Resources.DropCreateEmptyDatabaseSql);

                if (quietMode == false) Console.WriteLine("Creating {0} database tables, views, and stored procedures...", dbName);
                executeScript(server, dbName, Properties.Resources.CreateDatabaseSql);
            }

            // drop and create Peregrine database cleanup job
            if (createCleanupJob == true)
            {
                if (quietMode == false) Console.WriteLine("Installing scheduled job for {0} database cleanup...", dbName);
                executeScript(server, dbName, Properties.Resources.DropCreateCleanupJob);
            }

            // Close database connection
            if (quietMode == false) Console.WriteLine("Closing connection to SQL server...");
            connection.Close();
            if (quietMode == false) Console.WriteLine("Finished!");        
        }

        /// <summary>
        /// Executes an SQL script on the given server for a given
        /// database name.
        /// </summary>
        /// <param name="server">The SQL server intance.</param>
        /// <param name="dbName">Name of the database on given server.</param>
        /// <param name="script">The SQL script to run.</param>
        static void executeScript(Server server, string dbName, string script)
        {
            script = script + Environment.NewLine;
            // Search and replace database name in SQL script.
            // This does not currently protect from SQL injection.
            script = script.Replace(Properties.Resources.OldDatabaseName, dbName);
            server.ConnectionContext.ExecuteNonQuery(script);
        }
    }
}
