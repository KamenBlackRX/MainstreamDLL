using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.ReportingServices;
//using Microsoft.Reporting.WinForms;
using System.IO;
using System.Diagnostics;
using System.Data;

namespace Mainstream.Reports
{
    public class GenerateReports
    {
        public bool IsCompleted {get;set;}
        //Buffer to PDF
        byte[] _reportToPDF;
        //Dictonary to Param
        public Dictionary<string, string> _Paramters;
        //List of Parameters
   //     public List<ReportParameter> _ListReportParam;
        //Enum to Type
        public enum TypeOfService : int
        {
            Print = 1,
            Open = 2,
            Close = 3,
        }

        //private int timewrite = 0;

        public GenerateReports()
        {

        }
        /*
        public GenerateReports(Dictionary<string,string> Parametros)
        {
            _Paramters = new Dictionary<string, string>(Parametros);
        }

        public void StartGenerate(string path_report,string path_save,DataSet DataSetInclude, TypeOfService type)
        {
            IsCompleted = false;
            
            //Instancing ReportView
            ReportViewer report = new ReportViewer();
            //Processing Mode
            report.ProcessingMode = ProcessingMode.Local;
            //String to local of report
            report.LocalReport.ReportEmbeddedResource = path_report;
            //Instancing Parameters
            _ListReportParam = new List<ReportParameter>();
            //List of parameters
            foreach (KeyValuePair<string,string>  _paramkey in _Paramters)
            {
                _ListReportParam.Add(new ReportParameter(_paramkey.Key,_paramkey.Value));             
            }

            //Inserir Data Set
            report.LocalReport.DataSources.Clear();
            report.LocalReport.DataSources.Add(new ReportDataSource("DataSetProdutos", DataSetInclude));

            //Inserir Lista de parameters
            report.LocalReport.SetParameters(_ListReportParam);
            
            //Refresh all
      //      report.LocalReport.Refresh();


            //Variables to pass to PDF param
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            //Render Report
            _reportToPDF = report.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
            
            //Instancing Fs with use and asyncing
            FileStream fs = new FileStream(path_save, FileMode.Create, FileAccess.Write, FileShare.ReadWrite, _reportToPDF.Length);
	        
            //WriteFS
		    fs.WriteAsync(_reportToPDF, 0, _reportToPDF.Length);
            
            if (type == TypeOfService.Open)
            {
                Process.Start(path_save);
            }
            //Finish Generate
            IsCompleted = true;
            
        }
        */

        #region Dispose
        //Calling Dispose Methods
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // NOTE: Leave out the finalizer altogether if this class doesn't 
        // own unmanaged resources itself, but leave the other methods
        // exactly as they are. 
        ~GenerateReports()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (_reportToPDF != null) { _reportToPDF = null;}
          //      if (_ListReportParam != null) { _ListReportParam = null; }
                if (_Paramters != null) { _Paramters = null; }
            }
        }
        #endregion
    }

}
