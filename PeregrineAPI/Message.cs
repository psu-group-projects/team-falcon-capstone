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
    public class Message
    {
        int message_id = 0;
        int process_id = 0;
        double timestamp = 0;
        string message = "Null";
        string type = "Null";
        int priority = 0;
        
        [DataMember]
        public int MessageId
        {
            get { return message_id; }
            set { message_id = value; }
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
        public string MessageStr
        {
            get { return message; }
            set { message = value; }
        }

        //type could be shutdown, startup, general etc...
        //might want to make an enum out of this....
        [DataMember]
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        //priority could be some int where the higher the number, the more important.
        [DataMember]
        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }
    }
}