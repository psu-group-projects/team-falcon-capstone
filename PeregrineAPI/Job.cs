﻿using System;
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
        int job_id;
        int process_id;
        double timestamp;
        string job_name;
        int completed_count;
        int planned_count;

        public Job(int j_id, int p_id, double time, string j_name, int complete, int planned)
        {
            job_id = j_id;
            process_id = p_id;
            timestamp = time;
            job_name = j_name;
            completed_count = complete;
            planned_count = planned;
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