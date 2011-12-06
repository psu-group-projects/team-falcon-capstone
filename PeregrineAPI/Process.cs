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
    public class Process
    {
        int process_id = 0;
        string process_name = "Null";
        int state = 0;

        [DataMember]
        public int ProcessId
        {
            get { return process_id; }
            set { process_id = value; }
        }

        [DataMember]
        public string ProcessName
        {
            get { return process_name; }
            set { process_name = value; }
        }

        //state could be 0 for green, 1 for yellow or 2 for red.
        //this might need to be turned into an enum...
        [DataMember]
        public int State
        {
            get { return state; }
            set { state = value; }
        }
    }
}