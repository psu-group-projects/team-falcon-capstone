using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PeregrineDBWrapper;
using PeregrineAPI;

namespace PeregrinDBTestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            String testProcessName = "Test Process Name 1";
            ProcessState testState = ProcessState.RED;
            String testProcessMessage = "Test Process Message 1";
            Category testProcessCategory = Category.INFORMATION;
            Priority testProcessPriority = Priority.MEDIUM;
            
            String testJobName1 = "Test Job Name 1";
            double testPercent = 50;
            
            String testJobName2 = "Test Job Name 2";
            int testTotalTasks = 1000;
            int testCompleted = 300;

            DBLogWrapper log = new DBLogWrapper();

            log.logProcessStart(testProcessName);
            log.logProcessMessage(testProcessName, testProcessMessage, testProcessCategory, testProcessPriority);

            log.logJobStart(testJobName1, testProcessName);
            log.logJobProgressAsPercentage(testJobName1, testProcessName, testPercent);
            log.logJobComplete(testJobName1, testProcessName);

            log.logJobStartWithTotalTasks(testJobName2, testProcessName, testTotalTasks);
            log.logJobProgress(testJobName2, testProcessName, testTotalTasks, testCompleted);
            log.logJobComplete(testJobName2, testProcessName);

            log.logProcessStateChange(testProcessName, testState);
            log.logProcessShutdown(testProcessName);

            // Uncomment to pause console results at end.
            // Console.ReadLine();
        }
    }
}
