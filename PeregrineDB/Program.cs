/*
Author   : Weixiong Lu
Version  : 1.0.0
Date     : 01/06/2012
Copyright: Capstone Project Team Falcon 2012 All right reserved
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;



namespace PeregrineDB
{

    class Program
    {
        static void Main(string[] args)
        {
            
            //Connection String
            //PeregrineDBDataContext db = new PeregrineDBDataContext(@"Data Source=CAPSTONEBB;Initial Catalog=PeregrineDB;Integrated Security=True");
            PeregrineDBDataContext db = new PeregrineDBDataContext(Properties.Settings.Default.PeregrineDBConnectionString);


            //Get a typed table to run query
            var processes = db.GetTable1();

            //Attach log to sthow generated SQL
            db.Log = Console.Out;

            // Query all processes
            var processQuery =
                from Pro in processes
                select Pro;

            // Display Query result
            
            foreach (Process pro in processQuery)
            {
                Console.WriteLine("ProcessID = {0} ProcessName = {1} State = {2}", pro.ProcessID, pro.ProcessName, pro.State);
            }

            // Prevent from closing the console
            Console.ReadLine();
                
        }
    }
}
