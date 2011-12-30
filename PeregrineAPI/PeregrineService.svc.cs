using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PeregrineAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class PeregrineService : IPeregrineService
    {
        public Message getMessage(int msg_id)
        {
            if (msg_id == 0)
            {
                return new Message(1,1,1321654332,"process has booted up", 0, 0);
            }
            else if (msg_id == 1)
            {
                return new Message(2,3,1541633011,"Process has stopped unexpectedly", -1, 10);
            }
            return null;
        }

        public List<Process> getAllProcesses()
        {
            return new List<Process>()
            {
                new Process(1, "Compute PI", 0)
            };
        }

        public Job logJobStart(string process_name, string job_name, int job_count = 1)
        {
            /* Use this expression to get the unix timestamp:
             *
             * (DateTime.UtcNow - new DateTime(1970,1,1,0,0,0)).TotalSeconds;
             * 
             * and to convert a unix timestamp to a .NET DateTime:
             *     
             * (new DateTime(1970,1,1,0,0,0)).AddSeconds(unixtimestamp)
             * */
            
            return new Job(0, 0, (DateTime.UtcNow - new DateTime(1970,1,1,0,0,0)).TotalSeconds, job_name, job_count, 0);
        }

        public List<ProcessSummary> getProcessSummaryList(int start_id, int num_to_fetch, int sort_by)
        {
            //this is where some fetch would happen to the database.
            //execture stored proceedure.

            //put results into List of ProcessSummaries.

            return new List<ProcessSummary>()
            {
                new ProcessSummary(new Process(0, "make candy", 1), new Message(1, 0, 1234, "starting up", 1, 1)),
                new ProcessSummary(new Process(1, "solve world hunger", 1), new Message(2, 1, 12345, "error", -1, 1))
            };
        }
    }
}
