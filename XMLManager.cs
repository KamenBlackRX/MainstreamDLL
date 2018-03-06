using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Xml;
using System.IO;
namespace Mainstream.XML
{
    public class XMLManager
    {
        //Path to xML file
        string _path { get; set; }
        //Set async flag for read procedure
        bool _IsAsync { get; set; }

        //ctor

        /// <summary>
        /// Read XML Content and convert to dataset object
        /// </summary>
        /// <param name="path">The XML path to be readed</param>
        /// <param name="data">Data set to pass data by reference</param>
        public void ReadXMLFile(string path, ref DataSet data)
        {
            //verify if dataset is instancieated
            data = new DataSet();

            foreach (DataTable dataTable in data.Tables)
                dataTable.BeginLoadData();
            try
            {
                data.ReadXml(path);
            }
            catch(System.IO.FileNotFoundException ex)
            {
                Mainstream.NativeMethods.MessageBox(new IntPtr(0), "Error Ao ler o Xml\nMessagem: " + ex.Message, "Fatal Error:",0);
                throw;
            }

            foreach (DataTable dataTable in data.Tables)
                dataTable.EndLoadData();
                        
        }

        /// <summary>
        /// Read XML content and convert to string array
        /// </summary>
        /// <param name="path">XML File location to be loaded</param>
        /// <returns>Returns array of data</returns>
        public void DatasetToXML(ref DataSet data, string path)
        {
            foreach (DataTable dataTable in data.Tables)
                dataTable.BeginLoadData();
            try
            {
                data.WriteXml(path);
            }
            catch (System.IO.FileNotFoundException ex)
            {
                Mainstream.NativeMethods.MessageBox(new IntPtr(0), "Error Ao ler o Xml\nMessagem: " + ex.Message, "Fatal Error:", 0);
                throw;
            }

            foreach (DataTable dataTable in data.Tables)
                dataTable.EndLoadData();
        }
        

    }
}
