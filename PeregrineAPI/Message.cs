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
        int message_id;
        int process_id;
        DateTime timestamp; //we dont know what type this should be! need to figure this out.
        string message;
        Category category; //probly should be an enum (bootup, shutdown)
        Priority priority;

        public Message(int m_id, int p_id, DateTime time, string msg,  Category category, Priority priority)
        {
            message_id = m_id;
            process_id = p_id;
            timestamp = time;
            message = msg;
            this.category = category;
            this.priority = priority;
        }

        
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
        public DateTime Timestamp
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
        public int type
        {
            get { return type; }
            set { type = value; }
        }

        //priority could be some int where the higher the number, the more important.
        [DataMember]
        public Priority Priority
        {
            get { return priority; }
            set { priority = value; }
        }
    }
}