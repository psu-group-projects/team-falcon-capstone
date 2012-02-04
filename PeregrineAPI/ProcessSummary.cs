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

        ProcessDTO process;
        MessageDTO msg;

        public ProcessSummary(ProcessDTO p, MessageDTO m)
        {
            process = p;
            msg = m;
        }

        [DataMember]
        public ProcessDTO _process
        {
            get { return process; }
            set { }
        }

        [DataMember]
        public MessageDTO _message
        {
            get { return msg; }
            set { }
        }
    }
}