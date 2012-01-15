using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PeregrineDB;

namespace PeregrinDBTestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            int id;

            ProcessWrapper proc = new ProcessWrapper("TestClientProcess", 1);
            proc.PutInDatabase();

            Console.WriteLine("Put In Database: {0},{1},{2}", proc.ProcessID, proc.ProcessName, proc.State);

            id = proc.ProcessID;
            
            proc = new ProcessWrapper(id);

            Console.WriteLine("Pulled From Database: {0},{1},{2}", proc.ProcessID, proc.ProcessName, proc.State);

            Console.ReadLine();
        }
    }
}
