using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using PeregrineDBWrapper;
using PeregrineDB;
using System.Configuration;

namespace PeregrineAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class PeregrineService : IPeregrineService
    {
        private PeregrineDBDataContext db = new PeregrineDBDataContext();
        
        public Message getMessage(int msg_id)
        {
            var message_result = db.GetMessage(msg_id).ToList();

            Message message = message_result[0];
            return message;
        }

        public List<ProcessDTO> getAllProcesses()
        {
            throw new NotImplementedException();
        }

        public List<Message> getMessagesByProcessId(int processId, int pageSize, int pageNumber, SortBy sortBy, bool isShowStartUpAndShutdownCheckMarkEnabled)
        {
            List<Message> messages;

            if (isShowStartUpAndShutdownCheckMarkEnabled)
            {
                messages = db.GetStartStopMessagesWithProcessID(processId, (int)sortBy, 0, getStartIndex(pageNumber, pageSize), getEndIndex(pageNumber, pageSize)).ToList();
            }
            else
            {
                messages = db.GetMessagesWithProcessID(processId, (int)sortBy, 0, getStartIndex(pageNumber, pageSize), getEndIndex(pageNumber, pageSize)).ToList();
            }

            return messages;
        }

        
        public List<ProcessSummary> getSummaryByPage(int pageNumber, int numToFetch, SortBy sortBy)
        {
            int start = getStartIndex(pageNumber, numToFetch);
            int end = getEndIndex(pageNumber, numToFetch);
            //PeregrineDBDataContext db = new PeregrineDBDataContext();
            List<Process> processes = db.GetPageOfProcess(start, end).ToList<Process>();

            List<ProcessSummary> processSummaries = new List<ProcessSummary>();

            foreach (Process process in processes)
            {
                processSummaries.Add(
                    new ProcessSummary(
                        process,
                        db.GetTopMessageFromProcessId(process.ProcessID).First<Message>()
                    )
                );
            }
            return processSummaries;
        }

        private static int getEndIndex(int pageNumber, int numToFetch)
        {
            return (pageNumber * numToFetch) - 1;
        }

        private static int getStartIndex(int pageNumber, int numToFetch)
        {
            return (pageNumber - 1) * numToFetch;
        }

        public void logProcessMessage(string processName, string message, Category category, Priority priority)
        {
            throw new NotImplementedException();
        }

        public void logJobProgressAsPercentage(int jobID, string processName, double percent)
        {
            throw new NotImplementedException();
        }

        public void logJobProgress(int jobID, string processName, int total, int completed)
        {
            throw new NotImplementedException();
        }

        public void logJobStart(int jobID, string processName)
        {
            throw new NotImplementedException();
        }

        public void logJobStartWithTotalTasks(int jobID, string processName, int totalTasks)
        {
            throw new NotImplementedException();
        }

        public void logJobComplete(int jobID, string processName)
        {
            throw new NotImplementedException();
        }

        public void logProcessStart(string processName)
        {
            throw new NotImplementedException();
        }

        public void logProcessShutdown(string processName)
        {
            throw new NotImplementedException();
        }

        public void logProcessStateChange(string processName, ProcessState state)
        {
            throw new NotImplementedException();
        }


        //This method will be called automatically on some callback.
        //http://www.mikesdotnetting.com/Article/129/Simple-task-Scheduling-using-Global.asax
        public void cleanUpDatabase()
        {
            int interval = int.Parse(ConfigurationManager.AppSettings.Get("DB_Cleanup_Interval"));
            int process_min_life = int.Parse(ConfigurationManager.AppSettings.Get("Process_Min_Life_Time"));

            //call some stored proceedure to fetch the last cleanup datetime. (we will need to store this somewhere. maybe a file or a new table?)
            //if ((now time) - (last cleanup time) > interval){
            //  call stored proceedure to delete processes that are in a done state and (now - their datetime) > process_min_life
            //}
        }
    }
}

