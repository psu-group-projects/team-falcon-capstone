using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;

namespace PeregrineAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class PeregrineService : IPeregrineService
    {
        public Message getMessage(int msg_id)
        {
            throw new NotImplementedException();
        }

        public List<Process> getAllProcesses()
        {
            throw new NotImplementedException();
        }

        public List<ProcessSummary> getSummaryByPage(int pageNumber, int num_to_fetch, SortBy sortBy)
        {
            List<ProcessSummary> processSummaries = new List<ProcessSummary>();
            int i = 0;
            while (i < 100)
            {
                processSummaries.Add( new ProcessSummary(
                    new Process("falcon"+i, i, ProcessState.GREEN), 
                    new Message(i-60000,i,DateTime.Now,Path.GetRandomFileName(),Category.PROGRESS,Priority.LOW) ) );
                i++;
            }
            return processSummaries;
        }

        public void logProcessMessage(string processName, string message, Category category, Priority priority)
        {
            throw new NotImplementedException();
        }

        public void logJobProgressAsPercentage(int jobID, string processName, int percent)
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
    }
}

