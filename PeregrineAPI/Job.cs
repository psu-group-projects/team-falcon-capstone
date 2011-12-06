using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;

namespace PeregrineAPI
{
    [DataContract]
    public class Job
    {
        int job_id = 0;
        int process_id = 0;
        double timestamp = 0;
        string job_name = "Null";
        int completed_count = 0;
        int planned_count = 1;
        
        [DataMember]
        public int JobId
        {
            get { return job_id; }
            set { job_id = value; }
        }

        [DataMember]
        public int ProcessId
        {
            get { return process_id; }
            set { process_id = value; }
        }

        [DataMember]
        public double Timestamp
        {
            get { return timestamp; }
            set { timestamp = value; }
        }

        [DataMember]
        public string JobName
        {
            get { return job_name; }
            set { job_name = value; }
        }

        [DataMember]
        public int PlannedCount
        {
            get { return planned_count; }
            set { planned_count = value; }
        }

        [DataMember]
        public int CompletedCount
        {
            get { return completed_count; }
            set { completed_count = value; }
        }
    }
}