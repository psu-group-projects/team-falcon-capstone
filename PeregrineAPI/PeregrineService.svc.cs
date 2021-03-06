﻿using System;
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
            List<Message> message_result = db.GetMessage(msg_id).ToList();
            Message message;
            if (message_result.Count > 0)
            {
                message = message_result[0];
            }
            else
            {
                message = new Message();
            }
            return message;
        }

        public List<ProcessDTO> getAllProcesses()
        {
            throw new NotImplementedException();
        }

        /*
        public List<Message> getMessagesForMessageInq(
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
         */

        public List<GetPageOfMessageSummaryResult> getMessagesForMessageInq(String processName, int numToFetch, int priority, int getStartAndStop, SortBy sortBy, SortDirection sortDirection)
        {
            List<GetProcessIDFromNameResult> ids = db.GetProcessIDFromName(processName).ToList<GetProcessIDFromNameResult>();
            int id;
            if (ids.Count > 0){
                id = ids[0].ProcessID;
            }else{
                id = -1;
            }

            List<GetPageOfMessageSummaryResult> messages = db.GetPageOfMessageSummary(id, priority, getStartAndStop, (int)sortBy, (int)sortDirection, numToFetch).ToList<GetPageOfMessageSummaryResult>();
            return messages;
        }


        public List<GetPageOfProcessSummaryResult> getSummaryByPage(int pageNumber, int numToFetch, SortBy sortBy, SortDirection sortDirection)
        {
            int start = getStartIndex(pageNumber, numToFetch);
            int end = getEndIndex(pageNumber, numToFetch);
            //List<Process> processes = db.GetPageOfProcess(start, end).ToList<Process>();

            List<ProcessSummary> processSummaries = new List<ProcessSummary>();
            /*
            foreach (Process process in processes)
            {
                processSummaries.Add(
                    new ProcessSummary(
                        process,
                        db.GetTopMessageFromProcessId(process.ProcessID).First<Message>()
                    )
                );
            }*/

            List<GetPageOfProcessSummaryResult> summaries = db.GetPageOfProcessSummary((int)sortBy, (int)sortDirection, end).ToList<GetPageOfProcessSummaryResult>();

            /*
            foreach (GetPageOfProcessSummaryResult summary in summaries)
            {
                processSummaries.Add(new ProcessSummary(
                    new Process { ProcessName = summary.ProcessName, ProcessID = summary.ProcessID, State = summary.State },
                    new Message { Message1 = summary.LastMsg, Date = (System.DateTime)summary.MsgDate }
                ));
            }*/

            return summaries;
        }

        public List<Job> getPageOfJobsByProcessId(int processId, int pageNumber, int numToFetch)
        {
            int start = getStartIndex(pageNumber, numToFetch);
            int end = getEndIndex(pageNumber, numToFetch);
            return db.GetPageOfJobs(start, end, processId).ToList<Job>();   
        }

        public List<Message> getPageOfMessagesByProcessId(int processId, int pageNumber, int numToFetch)
        {
            int start = getStartIndex(pageNumber, numToFetch);
            int end = getEndIndex(pageNumber, numToFetch);
            return db.GetPageOfMessagesByProcessId(start, end, processId).ToList<Message>();
        }

        public List<GetProcessSummaryByNameResult> getProcessByName(String name)
        {
            List<GetProcessSummaryByNameResult> processSummaries;
            processSummaries = db.GetProcessSummaryByName(name).ToList<GetProcessSummaryByNameResult>();
            
            return processSummaries;
        }

        public List<Process> searchProcessByName(String name)
        {
            return db.SearchProcessByName(name).ToList<Process>();
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
    }
}

