using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Security.Cryptography;
using Mainstream.Cryptography;

namespace Mainstream.GUID
{

    public class GenerateGUID
    {
        
        public static string GetSerialNumber()
        {
           // Guid guid = Guid.NewGuid();
           // Guid guid = Guid.NewGuid();
            //Console.WriteLine("0x{1:X8}",guid.GetHashCode().ToString());
            
            //encrypt sistem
            CEncryptSystem.SHA1Encrypt _sha1 = new CEncryptSystem.SHA1Encrypt();
            //Return string
            string uniqueSerial = _sha1.GetSHA1Hash(GetInfo());
            return uniqueSerial;
        }
        private static string GetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(GetProcID());
            sb.Append(GetBiosID());
            return sb.ToString();
        }
        public static string GetProcID()
        {
            ManagementObjectCollection mbsList = null;
            ManagementObjectSearcher mbs = new ManagementObjectSearcher("Select * From Win32_processor");
            mbsList = mbs.Get();
            string id = "";
            foreach (ManagementObject mo in mbsList)
            {
                id = mo["ProcessorID"].ToString();
                break;
            }
            //Dispose and free resourses
            mbsList.Dispose();
            mbs.Dispose();
            
            mbs = null;
            mbsList = null;

            return id;
        }

        public static string GetBiosID()
        {
            ManagementObjectCollection mbsList = null;
            ManagementObjectSearcher mbs = new ManagementObjectSearcher("Select * From Win32_BIOS");
            mbsList = mbs.Get();
            StringBuilder sb = new StringBuilder();
            
            foreach (ManagementObject mo in mbsList)
            {
                sb.Append(mo.Properties["SMBIOSBIOSVersion"].Value.ToString());
                break;
            }
            //Dispose and free resourses
            mbsList.Dispose();
            mbs.Dispose();

            mbs = null;
            mbsList = null;

            return sb.ToString();
        }

        public static string GetHashSHA1()
        {
            //Instance SHA1
            SHA1 hash = SHA1.Create();
            //ENCODDING
            ASCIIEncoding encoding = new ASCIIEncoding();  
            //GET BYTES
            byte[] array = encoding.GetBytes(GetProcID());
            //COMPUTRASH
            array = hash.ComputeHash(array);
            
            //Stringbuild for Hash
            StringBuilder strHexa = new StringBuilder();

            //Foreache loop
            foreach (byte item in array)
            {
                // Convertendo  para Hexadecimal
                strHexa.Append(item.ToString("D"));
            }

            //Dispose elements
            hash.Dispose();
            encoding = null;
            array = null;
            
            //Return arry formated
            return strHexa.ToString();

        }  

    }
}
