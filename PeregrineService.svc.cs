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
                return new Message
                {
                    MessageId = 1,
                    ProcessId = 1,
                    Timestamp = 1321654332,
                    MessageStr = "Process has booted up.",
                    Type = "Boot",
                    Priority = 0,
                };
            }
            else if (msg_id == 1)
            {
                return new Message
                {
                    MessageId = 2,
                    ProcessId = 3,
                    Timestamp = 1541633011,
                    MessageStr = "Process has stopped unexpectedly.",
                    Type = "Error",
                    Priority = 10,
                };
            }
            return null;
        }

        public List<Process> GetAllProcesses()
        {
            return new List<Process>()
            {
                new Process
                {
                    ProcessId = 1,
                    ProcessName = "Compute PI",
                    State = 0,
                },
                new Process
                {
                    ProcessId = 2,
                    ProcessName = "Fluid Dynamics Sim",
                    State = 1,
                },
                new Process
                {
                    ProcessId = 3,
                    ProcessName = "Solve NP in P",
                    State = 2,
                },
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
            
            return new Job
            {
                JobId = 0,
                ProcessId = 0,
                Timestamp = (DateTime.UtcNow - new DateTime(1970,1,1,0,0,0)).TotalSeconds,
                JobName = job_name,
                PlannedCount = job_count,
                CompletedCount = 0
            };
        }
    }
}
