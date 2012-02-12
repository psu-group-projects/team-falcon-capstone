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

namespace PeregrineAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class PeregrineService : IPeregrineService
    {
        public MessageDTO getMessage(int msg_id)
        {
            throw new NotImplementedException();
        }

        public List<ProcessDTO> getAllProcesses()
        {
            throw new NotImplementedException();
        }

        public List<ProcessSummary> getSummaryByPage(int pageNumber, int numToFetch, SortBy sortBy)
        {
            int start = getStartIndex(pageNumber, numToFetch);
            int end = getEndIndex(pageNumber, numToFetch);
            PeregrineDBDataContext db = new PeregrineDBDataContext();
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


        public List<MessageDTO> getMessagesByProcessId(int processId, int pageSize, int pageNumber, SortBy sortBy, bool isShowStartUpAndShutdownCheckMarkEnabled)
        {
            throw new NotImplementedException();
        }
    }
}

