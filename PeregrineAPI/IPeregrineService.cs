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
        ASSENDING,
        [EnumMember]
        DESENDING
    }

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IPeregrineService
    {

        /**
         * UI Service Operations
         */
        [OperationContract]
        Message getMessage(int msg_id);

        [OperationContract]
        List<ProcessDTO> getAllProcesses();

        // This will be the main fetching method for the front page. gets summary objects.
        [OperationContract]
        List<ProcessSummary> getSummaryByPage(int pageNumber, int num_to_fetch, SortBy sortBy, SortDirection sortDirection); 

        //This hooks into the MsgInquryRepo
        [OperationContract]
        List<Message> getMessagesForMessageInq(
            int processId, 
            int pageSize, 
            int pageNumber, 
            SortBy sortBy, 
            SortDirection sortDirection,
            bool isShowStartUpAndShutdownCheckMarkEnabled);

        //This is the hook for the JobRepo
        [OperationContract]
        List<Job> getPageOfJobsByProcessId(int processId, int pageNumber, int numToFetch);

        //This is the hook for the MessageRepo
        [OperationContract]
        List<Message> getPageOfMessagesByProcessId(int processId, int pageNumber, int numToFetch);

        /**
         * This is for the client app
         */
        [OperationContract]
        void logProcessMessage(String processName, String message, Category category, Priority priority);

        [OperationContract]
        void logJobProgressAsPercentage(String jobName, String processName, double percent);

        [OperationContract]
        void logJobProgress(String jobName, String processName, int total, int completed);

        [OperationContract]
        void logJobStart(String jobName, String processName);

        [OperationContract]
        void logJobStartWithTotalTasks(String jobName, String processName, int totalTasks);

        [OperationContract]
        void logJobComplete(String jobName, String processName);

        [OperationContract]
        void logProcessStart(String processName);

        [OperationContract]
        void logProcessShutdown(String processName);

        [OperationContract]
        void logProcessStateChange(String processName, ProcessState state);
    }
}