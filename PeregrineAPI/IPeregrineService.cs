using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using PeregrineDB;

namespace PeregrineAPI
{

    //By default, stored proceedures order desc. This keeps most recent entrys up top.
    //Thus, order enums from lowset importance to highest (except for sortby).
    [DataContract]
    public enum Category
    {
        [EnumMember]
        START, //0
        [EnumMember]
        STOP, //1
        [EnumMember]
        INFORMATION, //2
        [EnumMember]
        STATE_CHANGE, //3
        [EnumMember]
        PROGRESS, //4
        [EnumMember]
        ERROR //5
    }

    [DataContract]
    public enum Priority
    {
        [EnumMember]
        LOW,
        [EnumMember]
        MEDIUM,
        [EnumMember]
        HIGH
    }

    [DataContract]
    public enum ProcessState
    {
        [EnumMember]
        GREEN,
        [EnumMember]
        YELLOW,
        [EnumMember]
        RED
    }

    [DataContract]
    public enum SortBy
    {
        [EnumMember]
        PROCESS_NAME, //0
        [EnumMember]
        PROCESS_STATE, //1
        [EnumMember]
        MESSAGE_CONTENT, //2
        [EnumMember]
        MESSAGE_DATE, //3
        [EnumMember]
        MESSAGE_CATEGORY, //4
        [EnumMember]
        MESSAGE_PRIORITY, //5
        [EnumMember]
        JOB_NAME, //6
        [EnumMember]
        JOB_PERCENT_COMPLETE //7
    }

    [DataContract]
    public enum SortDirection
    {
        [EnumMember]
        DESENDING,
        [EnumMember]
        ASSENDING
    }

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IPeregrineService
    {

        /// <summary>
        /// Gets a message by message id.
        /// </summary>
        /// <param name="msg_id">The message id.</param>
        /// <returns></returns>
        [OperationContract]
        Message getMessage(int msg_id);

        //This method is not implemented and not used anywere!
        [OperationContract]
        List<ProcessDTO> getAllProcesses();

        /// <summary>
        /// Used to fetch a list of process summary objects from the database. Used for the main page.
        /// </summary>
        /// <param name="pageNumber">A page number to fetch where a page is num_to_fetch in length.</param>
        /// <param name="num_to_fetch">The number to fetch and also the length of a page.</param>
        /// <param name="sortBy">The column to sort by specified with the SortBy Enum.</param>
        /// <param name="sortDirection">Sort ASC or DESC specified with the SortDirection Enum.</param>
        /// <returns></returns>
        [OperationContract]
        List<GetPageOfProcessSummaryResult> getSummaryByPage(int pageNumber, int num_to_fetch, SortBy sortBy, SortDirection sortDirection); 

        /// <summary>
        /// Used to fetch a list of message summary objects from the database. If no process is found with the provided name, an empty list is returned. Used for the message inquiry page.
        /// </summary>
        /// <param name="processName">A string that if non empty and the process exists, this will try to fetch messages that only belong to the process specified. Otherwise all messages are fetched limited by numToFetch.</param>
        /// <param name="numToFetch">The number to fetch.</param>
        /// <param name="priority">The priority that returned summaries should be. -1 will select any priority.</param>
        /// <param name="getStartAndStop">An int that when 1, only messages that have a start and stop category will be returned.</param>
        /// <param name="sortBy">The column to sort by specified with the SortBy Enum</param>
        /// <param name="sortDirection">Sort ASC or DESC specified with the SortDirection Enum.</param>
        /// <returns></returns>
        [OperationContract]
        List<GetPageOfMessageSummaryResult> getMessagesForMessageInq(String processName, int numToFetch, int priority, int getStartAndStop, SortBy sortBy, SortDirection sortDirection);

        /// <summary>
        /// Used to fetch a list of PeregrineDB.Job Objects. If the provided id doesnt match any processes, an empty list is returned. 
        /// </summary>
        /// <param name="processId">The process id that the fetched jobs belong to.</param>
        /// <param name="pageNumber">A page number to fetch where a page is num_to_fetch in length.</param>
        /// <param name="num_to_fetch">The number to fetch and also the length of a page.</param>
        /// <returns>List&lt;PeregrineDB.Job&gt;</returns>
        [OperationContract]
        List<Job> getPageOfJobsByProcessId(int processId, int pageNumber, int numToFetch);

        /// <summary>
        /// Used to fetch a list of PeregrineDB.Message Objects. If the provided id doesnt match any processes, an empty list is returned. 
        /// </summary>
        /// <param name="processId">The id of the process that the messages belong to.</param>
        /// <param name="pageNumber">A page number to fetch where a page is num_to_fetch in length.</param>
        /// <param name="num_to_fetch">The number to fetch and also the length of a page.</param>
        /// <returns></returns>
        [OperationContract]
        List<Message> getPageOfMessagesByProcessId(int processId, int pageNumber, int numToFetch);

        /// <summary>
        /// Used to fetch a process summary object contained in a list from the database where the process has the provided name. If no process is found with the provided name, an empty list is returned. Used for main page searching.
        /// </summary>
        /// <param name="name">The name of the process.</param>
        /// <returns></returns>
        [OperationContract]
        List<GetProcessSummaryByNameResult> getProcessByName(String name);

        /// <summary>
        /// Used to fetch Processes with names similar to the string provided. The main page autocomplete uses this method.
        /// </summary>
        /// <param name="name">The string to search for.</param>
        /// <returns></returns>
        [OperationContract]
        List<Process> searchProcessByName(String name);

        /// <summary>
        /// Logs the process message.
        /// </summary>
        /// <param name="processName">Name of the process.</param>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        /// <param name="priority">The priority.</param>
        [OperationContract]
        void logProcessMessage(String processName, String message, Category category, Priority priority);

        /// <summary>
        /// Logs the job progress as percentage.
        /// </summary>
        /// <param name="jobName">Name of the job.</param>
        /// <param name="processName">Name of the process.</param>
        /// <param name="percent">The percent.</param>
        [OperationContract]
        void logJobProgressAsPercentage(String jobName, String processName, double percent);

        /// <summary>
        /// Logs the job progress.
        /// </summary>
        /// <param name="jobName">Name of the job.</param>
        /// <param name="processName">Name of the process.</param>
        /// <param name="total">Total tasks to complete.</param>
        /// <param name="completed">New number of tasks completed.</param>
        [OperationContract]
        void logJobProgress(String jobName, String processName, int total, int completed);

        /// <summary>
        /// Logs the job start.
        /// </summary>
        /// <param name="jobName">Name of the job.</param>
        /// <param name="processName">Name of the process.</param>
        [OperationContract]
        void logJobStart(String jobName, String processName);

        /// <summary>
        /// Logs the job start with total tasks.
        /// </summary>
        /// <param name="jobName">Name of the job.</param>
        /// <param name="processName">Name of the process.</param>
        /// <param name="totalTasks">The total tasks.</param>
        [OperationContract]
        void logJobStartWithTotalTasks(String jobName, String processName, int totalTasks);

        /// <summary>
        /// Logs the job complete.
        /// </summary>
        /// <param name="jobName">Name of the job.</param>
        /// <param name="processName">Name of the process.</param>
        [OperationContract]
        void logJobComplete(String jobName, String processName);

        /// <summary>
        /// Logs the process start.
        /// </summary>
        /// <param name="processName">Name of the process.</param>
        [OperationContract]
        void logProcessStart(String processName);

        /// <summary>
        /// Logs the process shutdown.
        /// </summary>
        /// <param name="processName">Name of the process.</param>
        [OperationContract]
        void logProcessShutdown(String processName);

        /// <summary>
        /// Logs the process state change.
        /// </summary>
        /// <param name="processName">Name of the process.</param>
        /// <param name="state">The state.</param>
        [OperationContract]
        void logProcessStateChange(String processName, ProcessState state);
    }
}