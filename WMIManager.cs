using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Data;

namespace Mainstream
{
    public class WMIManager
    {
       public Mainstream.NativeMethods nm = new Mainstream.NativeMethods();

    }

    public class ProcessWMI : WMIManager
    {
        private DataSet data_r;

        public ProcessWMI()
        {

        }

        /// <summary>
        /// Get the current process from localmachine
        /// </summary>
        /// <param name="query">The content of WMI query</param>
        /// <returns>Return a string with process of local machine</returns>
        public string[] GetLocalProcess(string query)
        {
          
            //Instantiate WQL and Object manager 
            WqlObjectQuery wqlQuery = new WqlObjectQuery(query);
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wqlQuery);
            string[] arr_r = new string[searcher.Get().Count];
            int loop = 0;
            //Running loop to get data from ManagementObject class
            foreach (ManagementObject obj in searcher.Get())
            {

                arr_r[loop] = obj["Caption"].ToString();
                Console.WriteLine("Caption: {0}", obj["Caption"]);

                arr_r[loop] = obj["Description"].ToString();
                Console.WriteLine("Description: {0}", obj["Description"]);

                arr_r[loop] = obj["Name"].ToString();
                Console.WriteLine("Name: {0}", obj["Name"]);

                arr_r[loop] = obj["ProcessId"].ToString();
                Console.WriteLine("ProcessId: {0}", obj["ProcessId"]);

                //   l.Add("State", obj["State"].ToString());
                //     Console.WriteLine("State: {0}", obj["State"]);

                arr_r[loop] = obj["WorkingSetSize"].ToString();
                Console.WriteLine("WorkingSetSize: {0}", obj["WorkingSetSize"]);
                loop++;
            }
            return arr_r;       //return data colected from loop
        }
        /// <summary>
        /// Get Local machine Process to data Tabble
        /// </summary>
        /// <param name="query">WMI query to do select param</param>
        /// <returns>Dataset with process in local machine</returns>
        public DataSet GetLocalProcessToDataSet(string query)
        {
            //Instantiate WQL and Object manager 
            WqlObjectQuery wqlQuery = new WqlObjectQuery(query);
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(wqlQuery);
            DataSet data_r = new DataSet();
            int loop = 0;

            //Inster Table and colums
            data_r.Tables.Add("Process");
            //Add Colums for formatting data
            data_r.Tables[0].Columns.Add("Caption");
            data_r.Tables[0].Columns.Add("Description");
            data_r.Tables[0].Columns.Add("Name");
            data_r.Tables[0].Columns.Add("ProcessId");
            data_r.Tables[0].Columns.Add("WorkingSetSize");
            //Rows for data
            data_r.Tables[0].Rows.Add(typeof(string));
            //Loop to fill DataTable
            foreach (ManagementObject obj in searcher.Get())
            {
                data_r.Tables[0].Rows.Add(typeof(string));

                data_r.Tables[0].Rows[loop][0] = obj["Caption"].ToString();
                data_r.Tables[0].Rows[loop][1] = obj["Description"].ToString();
                data_r.Tables[0].Rows[loop][2] = obj["Name"].ToString();
                data_r.Tables[0].Rows[loop][3] = obj["ProcessId"].ToString();
                data_r.Tables[0].Rows[loop][4] = obj["WorkingSetSize"].ToString();
                loop++;
#if DEBUG
                Console.WriteLine("Caption: {0}", obj["Caption"]);
                Console.WriteLine("Description: {0}", obj["Description"]);
                Console.WriteLine("Name: {0}", obj["Name"]);
                Console.WriteLine("ProcessId: {0}", obj["ProcessId"]);
                Console.WriteLine("WorkingSetSize: {0}", obj["WorkingSetSize"]);
#endif
            }
            return data_r;
            
        }
        /// <summary>
        /// Get current process from a local machine
        /// </summary>
        /// <param name="query">The query for WMI</param>
        /// <param name="ComputerName">The IP or ComputerName for execute WMI query</param>
        /// <param name="connection">The connection parameter for query</param>
        /// <returns>The dataset with current process</returns>
        public DataSet GetLocalComputerProcess(string query, string ComputerName, ConnectionOptions connection)
        {
            

            //Management Scope object to WMI Scope
            //With tread-safe
            try
            {
                ManagementScope scope; //declarete scopte

                //Connections Options if is not provide one fully qualifiqued
                if (connection != null)
                {

                    scope = new ManagementScope(
                    "\\\\" + ComputerName + "\\root\\CIMV2");
                    scope.Connect();
                }
                else
                {
                    scope = new ManagementScope(
                        "\\\\" + ComputerName + "\\root\\CIMV2", connection);
                    scope.Connect();
                }
                //QueryObj and search for fectch
                ObjectQuery _query = new ObjectQuery(query);
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, _query);

                //Data-Set Initialize
                data_r = new DataSet();
                int loop = 0;
                //Inster Table and colums
                data_r.Tables.Add("Process");
                //Add Colums
                data_r.Tables[0].Columns.Add("Caption");
                data_r.Tables[0].Columns.Add("Description");
                data_r.Tables[0].Columns.Add("Name");
                data_r.Tables[0].Columns.Add("ProcessId");
                data_r.Tables[0].Columns.Add("WorkingSetSize");

                data_r.Tables[0].Rows.Add(typeof(string));

                //Depreciated -> Constants.Constants.Convert ct;

                foreach (ManagementObject obj in searcher.Get())
                {
                    data_r.Tables[0].Rows.Add(typeof(string));

                    //DEPRECIETED  data_r.Tables[0].Rows[loop][0] = obj["Caption"].ToString();
                    data_r.Tables[0].Rows[loop][1] = obj["Description"].ToString();
                    data_r.Tables[0].Rows[loop][2] = obj["Name"].ToString();
                    data_r.Tables[0].Rows[loop][3] = obj["ProcessId"].ToString();
                    data_r.Tables[0].Rows[loop][4] = Math.Round((Convert.ToDouble(obj["WorkingSetSize"]) / 1024)).ToString();
                    loop++;
#if DEBUG
                    Console.WriteLine("Caption: {0}", obj["Caption"]);
                    Console.WriteLine("Description: {0}", obj["Description"]);
                    Console.WriteLine("Name: {0}", obj["Name"]);
                    Console.WriteLine("ProcessId: {0}", obj["ProcessId"]);
                    Console.WriteLine("WorkingSetSize: {0}", obj["WorkingSetSize"]);
#endif
                }
              
            }
            catch (ManagementException err)
            {

                Mainstream.NativeMethods.MessageBox(new IntPtr(0),"An error occured while querying for WMI data: " + err.Message,"Error:",0);
                
            }
            catch (System.UnauthorizedAccessException unauthorizedErr)
            {
                Mainstream.NativeMethods.MessageBox(new IntPtr(0),"Connection error " +
                    "(user name or password might be incorrect): " +
                    unauthorizedErr.Message,"Error",0);
                throw;
            }
            //Return data set after query
            return data_r;
        }
        /// <summary>
        /// Dispose elemente to free memory
        /// </summary>
        /// <param name="disposing">Get dispose status</param>
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if(data_r !=null ) { data_r.Dispose(); }

            }
            this.Dispose(disposing);
        }
    }

    
}
