using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PeregrineAPI
{
    [DataContract]
    public enum Category
    {
        [EnumMember]
        ERROR,
        [EnumMember]
        INFORMATION,
        [EnumMember]
        STATE_CHANGE,
        [EnumMember]
        START,
        [EnumMember]
        STOP,
        [EnumMember]
        PROGRESS
    }

    [DataContract]
    public enum Priority
    {
        [EnumMember]
        HIGH,
        [EnumMember]
        MEDIUM,
        [EnumMember]
        LOW
    }

    [DataContract]
    public enum ProcessState
    {
        [EnumMember]
        RED,
        [EnumMember]
        GREEN,
        [EnumMember]
        YELLOW
    }

    [DataContract]
    public enum SortBy
    {
        [EnumMember]
        PROCESS_NAME,
        [EnumMember]
        LAST_MESSAGE,
        [EnumMember]
        MESSAGE_DATE,
        [EnumMember]
        PROCESS_STATE
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
        List<Process> getAllProcesses();

        // This will be the main fetching method for the front page. gets summary objects.
        [OperationContract]
        List <ProcessSummary> getSummaryByPage(int pageNumber, int num_to_fetch, SortBy sortBy); 


        /**
         * This is for the client app
         */
        [OperationContract]
        void logProcessMessage(String processName, String message, Category category, Priority priority);

        [OperationContract]
        void logJobProgressAsPercentage(int jobID, String processName, int percent);

        [OperationContract]
        void logJobProgress(int jobID, String processName, int total, int completed);

        [OperationContract]
        void logJobStart(int jobID, String processName);

        [OperationContract]
        void logJobStartWithTotalTasks(int jobID, String processName, int totalTasks);

        [OperationContract]
        void logJobComplete(int jobID, String processName);

        [OperationContract]
        void logProcessStart(String processName);

        [OperationContract]
        void logProcessShutdown(String processName);

        [OperationContract]
        void logProcessStateChange(String processName, ProcessState state);
    }
}