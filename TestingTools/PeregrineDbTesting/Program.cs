using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;
using System.IO;
using System.Text.RegularExpressions;

namespace PeregrineDbTesting
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
            FileInfo file;
            string script;
            string reply;

            Console.WriteLine("Creating a new PeregrineDB will completely overwrite your current PeregrineDB (if it exists).");
            Console.WriteLine("Are you sure you wish to do this? (yes/no)");

            reply = Console.ReadLine();

            if (reply == "yes")
            {

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder["Data Source"] = "(local)";
                builder["integrated Security"] = true;

                Console.WriteLine("Connecting to SQL server using \"{0}\" connection string...", builder.ConnectionString);

                SqlConnection connection = new SqlConnection(builder.ConnectionString);

                connection.Open();

                Console.WriteLine("Creating database...");

                file = new FileInfo("C:\\falcon\\source\\TestingTools\\PeregrineDbTesting\\SQL\\DropCreatePeregrineDB.sql");
                script = file.OpenText().ReadToEnd();
                executeSqlStrings(connection, script);

                Console.WriteLine("Creating tables...");

                file = new FileInfo("C:\\falcon\\source\\TestingTools\\PeregrineDbTesting\\SQL\\DropCreateMessagesTable.sql");
                script = file.OpenText().ReadToEnd();
                executeSqlStrings(connection, script);

                file = new FileInfo("C:\\falcon\\source\\TestingTools\\PeregrineDbTesting\\SQL\\DropCreateProcessTable.sql");
                script = file.OpenText().ReadToEnd();
                executeSqlStrings(connection, script);

                file = new FileInfo("C:\\falcon\\source\\TestingTools\\PeregrineDbTesting\\SQL\\DropCreateJobTable.sql");
                script = file.OpenText().ReadToEnd();
                executeSqlStrings(connection, script);

                file = new FileInfo("C:\\falcon\\source\\TestingTools\\PeregrineDbTesting\\SQL\\DropCreateLogRelTable.sql");
                script = file.OpenText().ReadToEnd();
                executeSqlStrings(connection, script);

                Console.WriteLine("Closing connection to SQL server...");

                connection.Close();

                Console.WriteLine("Finished!");
            }
        }

        // Commands can only be one batch. Batches are seperated by GO statements.
        static string[] splitSqlAtGo(string commandString)
        {
            string[] commands = Regex.Split(commandString, "GO\r\n");
            return commands;
        }

        static void executeSqlStrings(SqlConnection connection, string script)
        {
            string[] commandtexts = splitSqlAtGo(script);

            foreach (string commandtext in commandtexts)
            {
                //Console.WriteLine("Splt--------------");
                //Console.WriteLine(commandtext);
                SqlCommand command = new SqlCommand(commandtext, connection);
                command.ExecuteNonQuery();
            }
        }

    }
}
