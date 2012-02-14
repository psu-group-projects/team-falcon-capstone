using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.Text.RegularExpressions;

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
                    if (String.Compare(dbName, Properties.Resources.OldDatabaseName, true) == 0) Console.WriteLine("\nWarning! {0} is the name of the master database. If you are executing this on the capstone lab server, DO NOT CONTINUE. The master database would be overwritten!", Properties.Resources.OldDatabaseName);
                    okayToGo = true;
                }
            }

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

                Console.WriteLine("Dropping old and creating empty {0} database...", dbName);
                executeScript(connection, dbName, Properties.Resources.DropCreateEmptyDatabaseSql);

                Console.WriteLine("Creating {0} database tables, views, and stored procedures...", dbName);
                executeScript(connection, dbName, Properties.Resources.CreateDatabaseSql);

                Console.WriteLine("Closing connection to SQL server...");
                connection.Close();

                Console.WriteLine("Finished!");
            }
        }

        static void executeScript(SqlConnection connection, string dbName, string script)
        {
            script = script + Environment.NewLine;
            script = script.Replace(Properties.Resources.OldDatabaseName, dbName);
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
