using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace Mainstream.Stream
{
    public class Mainstream_Stream : IDisposable
    {
        //Buffer size
        public int file_lenght;
        //byte[] _bytems;
        /// <summary>
        /// Load Blob Image
        /// </summary>
        /// <param name="img">Buffer of Image</param>
        /// <returns>In memory image buffer</returns>
        public MemoryStream f_open_blob_image (byte[] img)
        {
            
            try
            {
              //Ctor with buffer
              MemoryStream retVal = new MemoryStream(img);
            //return buffer
              return retVal;
              
            }
            catch (Exception ex)
            {
                Mainstream.NativeMethods.MessageBox(new IntPtr(0), "A Message error has benn occured" + ex.ToString(), "Error",0);
                throw;
            }
            
        }

        public void Dispose()
        {
     
        }
    }
}
