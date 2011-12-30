using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using System.Data.Linq;
using System.Data.Linq.Mapping;


namespace PeregrineDB_WinForm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            //Connection String
            PeregrineDB_formDataContext db = new PeregrineDB_formDataContext(@"Data Source=CAPSTONEBB;Initial Catalog=PeregrineDB;Integrated Security=True");

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
                Console.WriteLine("ProcessID = {0} ProcessName = {1} State = {2} ", pro.ProcessID, pro.ProcessName, pro.State);
            }

            // Prevent from closing the console
            Console.ReadLine();
                
         }
    }
}
