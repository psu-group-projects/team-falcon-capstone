using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PeregrineDB;

namespace PeregrinDBTestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            int id;

            ProcessWrapper proc = new ProcessWrapper("TestClientProcess", 1);
            proc.PutInDatabase();
            Console.WriteLine("Put In Database: {0},{1},{2}", proc.ProcessID, proc.ProcessName, proc.State);
            id = proc.ProcessID;
            proc = new ProcessWrapper(id);
            Console.WriteLine("Pulled From Database: {0},{1},{2}", proc.ProcessID, proc.ProcessName, proc.State);
            proc.DeleteFromDatabase();
            Console.WriteLine("Deleted From Database: {0},{1},{2}", proc.ProcessID, proc.ProcessName, proc.State);

            JobWrapper job = new JobWrapper("TestClientJob", 1, 10, 0.1);
            job.PutInDatabase();
            Console.WriteLine("Put In Database: {0},{1},{2},{3},{4}", job.JobID, job.JobName, job.PlannedCount, job.CompletedCount, job.PercentComplete);
            id = job.JobID;
            job = new JobWrapper(id);
            Console.WriteLine("Pulled From Database: {0},{1},{2},{3},{4}", job.JobID, job.JobName, job.PlannedCount, job.CompletedCount, job.PercentComplete);
            job.DeleteFromDatabase();
            Console.WriteLine("Deleted From Database: {0},{1},{2},{3},{4}", job.JobID, job.JobName, job.PlannedCount, job.CompletedCount, job.PercentComplete);

            MessageWrapper mess = new MessageWrapper("TestClientMessage", DateTime.Now, 1, 1);
            mess.PutInDatabase();
            Console.WriteLine("Put In Database: {0},{1},{2},{3},{4}", mess.MessageID, mess.Message1, mess.Date, mess.Category, mess.Priority);
            id = mess.MessageID;
            mess = new MessageWrapper(id);
            Console.WriteLine("Pulled From Database: {0},{1},{2},{3},{4}", mess.MessageID, mess.Message1, mess.Date, mess.Category, mess.Priority);
            mess.DeleteFromDatabase();
            Console.WriteLine("Deleted From Database: {0},{1},{2},{3},{4}", mess.MessageID, mess.Message1, mess.Date, mess.Category, mess.Priority);

            Console.ReadLine();
        }
    }
}
