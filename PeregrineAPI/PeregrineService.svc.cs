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
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public Message GetMessage(int msg_id)
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

        public List<Process> GetAllProcesses()
        {
            return new List<Process>()
            {
                new Process(1, "Compute PI", 0)
            };
        }

        public Job LogJobStart(string process_name, string job_name, int job_count = 1)
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
    }
}
