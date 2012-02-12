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
        private DBLogWrapper logWrapper = new DBLogWrapper();

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

        public List<Message> getMessagesByProcessId(
            int processId, 
            int pageSize, 
            int pageNumber, 
            SortBy sortBy, 
            SortDirection sortDirection, 
            bool isShowStartUpAndShutdownCheckMarkEnabled)
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

        
        public List<ProcessSummary> getSummaryByPage(int pageNumber, int numToFetch, SortBy sortBy, SortDirection sortDirection)
        {
            int start = getStartIndex(pageNumber, numToFetch);
            int end = getEndIndex(pageNumber, numToFetch);
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

        public List<Job> getListOfJobsByProcessName(int pageNumber, int numToFetch, string processName)
        {
            int start = getStartIndex(pageNumber, numToFetch);
            int end = getEndIndex(pageNumber, numToFetch);
            return null;   
        }

        public List<MessageDTO> getMessagesByProcessName(string searchpattern, int pageNumber, int numToFetch, SortBy sortBy, SortDirection sortDirection)
        {
            throw new NotImplementedException();
        }

        private static int getEndIndex(int pageNumber, int numToFetch)
        {
            return (pageNumber * numToFetch) - 1;
        }

        private static int getStartIndex(int pageNumber, int numToFetch)
        {
            return (pageNumber - 1) * numToFetch;
        }


        /**
         * Below is for the inbound client logging
         */

        public void logProcessMessage(string processName, string message, Category category, Priority priority)
        {
            logWrapper.logProcessMessage(processName, message, category, priority);
        }

        public void logJobProgressAsPercentage(String jobName, string processName, double percent)
        {
            logWrapper.logJobProgressAsPercentage(jobName, processName, percent);
        }

        public void logJobProgress(String jobName, string processName, int total, int completed)
        {
            logWrapper.logJobProgress(jobName, processName, total, completed);
        }

        public void logJobStart(String jobName, string processName)
        {
            logWrapper.logJobStart(jobName, processName);
        }

        public void logJobStartWithTotalTasks(String jobName, string processName, int totalTasks)
        {
            logWrapper.logJobStartWithTotalTasks(jobName, processName, totalTasks);
        }

        public void logJobComplete(String jobName, string processName)
        {
            logWrapper.logJobComplete(jobName, processName);
        }

        public void logProcessStart(string processName)
        {
            logWrapper.logProcessStart(processName);
        }

        public void logProcessShutdown(string processName)
        {
            logWrapper.logProcessShutdown(processName);
        }

        public void logProcessStateChange(string processName, ProcessState state)
        {
            logWrapper.logProcessStateChange(processName, state);
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

