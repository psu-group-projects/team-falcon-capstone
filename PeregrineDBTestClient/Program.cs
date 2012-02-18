using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using PeregrineAPI;
//using PeregrineDB;
using PeregrineDBWrapper;

namespace PeregrinDBTestClient
{
    class Program
    {
        private static DBLogWrapper log = new DBLogWrapper();
        private static Random ran = new Random();

        public static int num_of_processes = 100;
        public static int max_tasks_per_job = 100;
        public static int average_tasks_completed_per_step = 50;
        public static int num_procs_made = 0;
        public static int num_jobs_made = 0;
        public static int max_simulation_iters = num_of_processes*10;

        public static Dictionary<int, Proc> newProcs = new Dictionary<int, Proc>();
        public static Dictionary<int, ProcJob> newProcJobs = new Dictionary<int, ProcJob>();
        
        static void Main(string[] args)
        {
            long ticks = DateTime.Now.Ticks;
            int baseNameID = (int)(((ticks / 1000000000.0) - Math.Floor(ticks / 1000000000.0)) * 100000);

            int num_loops = 0;

            //start simulation loop:
            while (num_procs_made < num_of_processes && num_loops < max_simulation_iters)
            {
                //roll the dice to select next action:
                int next_action = ran.Next(1, 100);

                int newProcChance = Math.Max(33, (100 - (num_procs_made * (300 / num_of_processes))));
                int insertJobChance = 5*(100-newProcChance)/16;
                int progressJobChance = 9*(100-newProcChance)/16;
                //int shutdownProcChance = 100-insertJobChance-progressJobChance;
                //Console.WriteLine("   shutdown chance: 100- "+(newProcChance+insertJobChance+progressJobChance));

                if (next_action < newProcChance)
                {
                    //insert process
                    int newBaseNameID = (baseNameID + num_procs_made);
                    String newProcName = "Sample Process " + newBaseNameID;
                    insertProc(newProcName);
                }
                else if(next_action < (newProcChance+insertJobChance))
                {
                    //insert job
                    if (newProcs.Count > 0)
                    {
                        Proc p = getRandomProc();
                        insertJob(p);
                    }
                }
                else if (next_action < (newProcChance + insertJobChance + progressJobChance))
                {
                    //simulate job
                    if (newProcs.Count > 0 && newProcJobs.Count > 0)
                    {
                        ProcJob j = getRandomJob();
                        Proc p = newProcs[j.procKey];
                        bool completed = false;
                        if (j.completedCount < j.plannedCount)
                        {
                            //job is not yet finished.
                            j.completedCount += ran.Next(average_tasks_completed_per_step / 3, average_tasks_completed_per_step * 3);
                            Console.WriteLine("Simulating Job: " + j.jobName + " in process: " + p.procName + " new progress: " + j.completedCount + "/" + j.plannedCount);
                            if (j.completedCount < j.plannedCount)
                            {
                                //job still in progress
                                log.logJobProgress(j.jobName, p.procName, j.plannedCount, j.completedCount);
                            }
                            else
                            {
                                //job just finished
                                log.logJobComplete(j.jobName, p.procName);
                                completed = true;
                                Console.WriteLine("Job: " + j.jobName + " Done!");
                            }

                            if (!completed)
                            {
                                //job not done, roll for error
                                int error_roll = ran.Next(1, 100);
                                if (error_roll < 5)
                                {
                                    p.procState = ProcessState.RED;
                                    log.logProcessMessage(p.procName, "Fatal Error on job:" + j.jobName, Category.ERROR, Priority.HIGH);
                                    log.logProcessStateChange(p.procName, p.procState);
                                    Console.WriteLine("Job: " + j.jobName + " gave process fatal error!");
                                }
                                else if (error_roll < 15)
                                {
                                    if (p.procState != ProcessState.RED)
                                    {
                                        p.procState = ProcessState.YELLOW;
                                    }
                                    log.logProcessMessage(p.procName, "Problem on job:" + j.jobName, Category.ERROR, Priority.MEDIUM);
                                    log.logProcessStateChange(p.procName, p.procState);
                                    Console.WriteLine("Job: " + j.jobName + " gave process medium error!");
                                }
                            }
                        }
                        else
                        {
                            //job is already finished. so insert a new one instead.
                            insertJob(p);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Tried to simulate a job but there are none! Trying to insert one!");
                        if (newProcs.Count > 0)
                        {
                            Proc p = getRandomProc();
                            insertJob(p);
                        }
                        else
                        {
                            Console.WriteLine("hmm.. ok... no processes either... lets add one of those then!");
                            int newBaseNameID = (baseNameID + num_procs_made);
                            String newProcName = "Sample Process " + newBaseNameID;
                            insertProc(newProcName);
                        }
                    }
                }
                else
                {
                    //shutdown proc
                    Proc p = getRandomProc();
                    log.logProcessShutdown(p.procName);
                    newProcs.Remove(p.keyInMap);
                    Console.WriteLine("Process: " + p.procName + " is shutting down.");
                    List<int> keysToRemove = new List<int>();
                    foreach(KeyValuePair<int,ProcJob> j in newProcJobs)
                    {
                        if (j.Value.procKey == p.keyInMap)
                        {
                            //had to do it this weird way because an exception is created when you modify a collection when iterating over it.
                            keysToRemove.Add(j.Value.keyInMap);
                            Console.WriteLine("Removing job from map: " + j.Value.jobName);
                        }
                    }

                    foreach (int key in keysToRemove)
                    {
                        newProcJobs.Remove(key);
                    }
                }

                ++num_loops;

                Console.WriteLine("Starting Loop Iteration: " + num_loops + " ==============================");

                System.Threading.Thread.Sleep(200);
            }

            if (num_loops >= max_simulation_iters)
            {
                Console.WriteLine("DONE! However loop broke to iteration limit!");
            }
            else
            {
                Console.WriteLine("DONE!!! ALL "+num_of_processes+" Processes Simulated!");
            }

            
            //ISingleResult<InsertProcessResult> insResult = db.InsertProcess(-1, procName, (int)GlobVar.DEFAULT_PROCESS_STATE);
            //InsertProcessResult proc = insResult.First();
            //ProcessId = proc.ProcessID;

            /*String testProcessName = "Test Process Name 1";
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
            log.logProcessShutdown(testProcessName);*/

            // Uncomment to pause console results at end.
            Console.ReadLine();
        }

        public static void insertProc(String name)
        {
            log.logProcessStart(name);
            newProcs.Add(num_procs_made, new Proc(name, num_procs_made));
            ++num_procs_made;
            Console.WriteLine("Inserted new Proc (#"+num_procs_made+"): " + name);
        }

        public static void insertJob(Proc p)
        {
            int newJobTasks = ran.Next(max_tasks_per_job) + 1;
            String newJobName = "Job "+p.jobCount+" For " + p.procName;
            log.logJobStartWithTotalTasks(newJobName, p.procName, newJobTasks);
            //p.jobs.Add(new ProcJob(newJobName, newJobTasks, 0, p.locationInList));
            newProcJobs.Add(num_jobs_made, new ProcJob(newJobName, newJobTasks, 0, p.keyInMap, num_jobs_made));
            p.jobCount += 1;
            ++num_jobs_made;
            Console.WriteLine("Inserted new Job: " + newJobName);
        }

        public static Proc getRandomProc()
        {
            List<KeyValuePair<int, Proc>> list = newProcs.ToList();
            int get = ran.Next(0, list.Count);
            return list[get].Value;
        }

        public static ProcJob getRandomJob()
        {
            List<KeyValuePair<int, ProcJob>> list = newProcJobs.ToList();
            int get = ran.Next(0, list.Count);
            return list[get].Value;
        }
    }

    class Proc
    {
        public String procName;
        public ProcessState procState;
        //public List<ProcJob> jobs;
        public int keyInMap;
        public int jobCount;

        public Proc(String name, int location)
        {
            this.procName = name;
            //this.jobs = new List<ProcJob>();
            this.procState = ProcessState.GREEN;
            this.keyInMap = location;
            this.jobCount = 0;
        }
    }

    class ProcJob
    {
        public String jobName;
        public int plannedCount;
        public int completedCount;
        public int procKey;
        public int keyInMap;

        public ProcJob(String name, int planned, int completed, int location, int key)
        {
            this.jobName = name;
            this.plannedCount = planned;
            this.completedCount = completed;
            this.procKey = location;
            this.keyInMap = key;
        }

    }
}
