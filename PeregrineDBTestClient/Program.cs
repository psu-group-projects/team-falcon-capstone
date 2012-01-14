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
            // ProcessID 1 should already exist in DB.
            ProcessWrapper proc = new ProcessWrapper(1);

            Console.WriteLine("{0}, {1}, {2}", proc.ProcessID, proc.ProcessName, proc.State);
            Console.ReadLine();
        }
    }
}
