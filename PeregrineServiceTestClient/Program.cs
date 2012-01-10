using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PeregrineAPI;

namespace PeregrineServiceTestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            List<ProcessSummary> list;
            PeregrineService service = new PeregrineService();
            list = service.getProcessSummaryList(0, 5, 1);
            //Process p = service.searchForProcess("build", 0, 10, sortenum);

            foreach (ProcessSummary item in list)
            {
                Console.WriteLine(item._message.MessageStr);
            }
        }
    }
}
