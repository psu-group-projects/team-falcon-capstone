using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;



namespace PeregrineDB
{
    /*
    [Table(Name = "Process")]
    public class Process
    {
        private int _ProcessID;
        [Column(IsPrimaryKey = true, Storage = "_ProcessID")]
        public int ProcessID
        {
            get
            {
                return this._ProcessID;

            }

            set
            {
                this._ProcessID = value;
            }
        }


        private string _ProcessName;
        [Column(Storage = "_ProcessName")]
        public string ProcessName
        {
            get
            {
                return this._ProcessName;

            }

            set
            {
                this._ProcessName = value;
            }
        }


        private int _State;
        [Column(Storage = "_State")]
        public int State
        {
            get
            {
                return this._State;

            }

            set
            {
                this._State = value;
            }
        }

    }

     */

    class Program
    {
        static void Main(string[] args)
        {
            
            //Connection String
            PeregrineDBDataContext db = new PeregrineDBDataContext(@"Data Source=CAPSTONEBB;Initial Catalog=PeregrineDB;Integrated Security=True");

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
