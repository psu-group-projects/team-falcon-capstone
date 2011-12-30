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
    public class ProcessSummary
    {
        /*
         * ProcessSummary will contain: Process process, Message msg
         */

        Process process;
        Message msg;

        public ProcessSummary(Process p, Message m)
        {
            process = p;
            msg = m;
        }

        [DataMember]
        public Process _process
        {
            get { return process; }
            set { }
        }

        [DataMember]
        public Message _message
        {
            get { return msg; }
            set { }
        }
    }
}