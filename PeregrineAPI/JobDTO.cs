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
    public class JobDTO
    {
        int job_id;
        int process_id;
        double timestamp;
        string job_name;
        double percentComplete;
        int plannedCount;

        public JobDTO()
        {
        }

        public JobDTO(int j_id, int p_id, double time, string j_name, double complete, int planned)
        {
            job_id = j_id;
            process_id = p_id;
            timestamp = time;
            job_name = j_name;
            percentComplete = complete;
            plannedCount = planned;
        }
        
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
            get { return plannedCount; }
            set { plannedCount = value; }
        }

        [DataMember]
        public double PercentComplete
        {
            get { return percentComplete; }
            set { percentComplete = value; }
        }
    }
}