using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestModel.Models
{
    public class Message
    {
        public Message(){}


        public int ID { get; set;}
        public string content { get; set;}
        public string processName { get; set;}
        public int priority { get; set; }
        public DateTime Date { get; set;}
    }
}