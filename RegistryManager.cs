using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Mainstream.RegKey
{
    public class RegistryManager
    {
        RegistryKey key;

        public RegistryKey Key
        {
            get
            {
                return key;
            }

            set
            {
                key = value;
            }
        }
        /// <summary>
        /// Verify if registry is created
        /// </summary>
        /// <returns>Return the value of asnwer</returns>
        public bool CreateRegistry()
        {
            try
            {
                Key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Mainstream\\UMS\\Version",false);
                if(Key == null)
                {
                    Registry.CurrentUser.CreateSubKey("SOFTWARE\\Mainstream\\UMS\\Version");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        /// <summary>
        /// Write Registry Key and its value
        /// </summary>
        /// <param name="caption">The value of key in it.</param>
        /// <param name="isWritable">Protect for RO(Read Only)</param>
        public void WriteRegistry(string caption, bool isWritable)
        {

        }

        public string[] ReadSubKey( string[] value)
        {
            string[] reval = new string[2];
            reval[0] =  (string) key.GetValue(value[0], value[0], RegistryValueOptions.None);
            return reval;
        }

    }


}
