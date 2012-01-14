using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace PeregrineAPI
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IPeregrineService
    {

        // TODO: Add your service operations here
        [OperationContract]
        Message getMessage(int msg_id);

        [OperationContract]
        List<Process> getAllProcesses();

        [OperationContract]
        Job logJobStart(string process_name, string job_name, int job_count = 1);
        
        // This will be the main fetching method for the front page. gets summary objects.
        [OperationContract]
        List <ProcessSummary> getProcessSummaryList(int start_id, int num_to_fetch, int sort_by); //sort_by should be an enum.
    }
}
