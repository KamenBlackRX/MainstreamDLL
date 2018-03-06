using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Mainstream.DiskManager
{
    public class DiskManager
    {

        public DiskManager()
        {

        }
        /// <summary>
        /// Get string array of Disk info
        /// </summary>
        public Dictionary<string, string> GetDiskLocalInfo()
        {
            //Get all drives from machine
            DriveInfo[] allDrives = DriveInfo.GetDrives();

            Dictionary<string, string> _retval = new Dictionary<string, string>();

            //Loop from disk
            try
            {
                foreach (DriveInfo drive in allDrives)
                {
                    //Add Drive Info to dict
                    _retval.Add("DriveName", drive.Name);
                    _retval.Add("DriveType", drive.DriveType.ToString());

                    //Info
                    Console.WriteLine("Drive {0}", drive.Name);
                    Console.WriteLine(" Drive type: {0}", drive.DriveType);
                    //Loop for drive
                    if (drive.IsReady == true)
                    {

                        //Add to dictonary the drive parameters
                        _retval.Add("DriveVolumeLabel", drive.Name + " = " + drive.VolumeLabel);
                        _retval.Add("DriveFileSystem", drive.Name + " = " + drive.DriveFormat);
                        _retval.Add("DriveAvaliableSpace", drive.Name + " = " + drive.AvailableFreeSpace);
                        _retval.Add("DriveTotalFreeSpace", drive.Name + " = " + drive.TotalFreeSpace);
                        _retval.Add("DriveTotalSize", drive.Name + " = " + drive.TotalSize);

                        //Info to Show DEBUG
#if DEBUG
                        Console.WriteLine("  Volume label: {0}", drive.VolumeLabel);
                        Console.WriteLine("  File system: {0}", drive.DriveFormat);
                        Console.WriteLine(
                            "  Available space to current user:{0, 15} bytes",
                            drive.AvailableFreeSpace);

                        Console.WriteLine(
                            "  Total available space:          {0, 15} bytes",
                            drive.TotalFreeSpace);

                        Console.WriteLine(
                            "  Total size of drive:            {0, 15} bytes ",
                            drive.TotalSize);
                    }
                }
            }
            catch (SystemException ex)
            {
                Console.WriteLine("Error: " + ex.HResult + "\nError Message: " + ex.Message + "!");
            }
            //Return the dictonary data formated
            return _retval;
        }
#endif

#if !DEBUG
                    }
                }
            }
            catch (SystemException ex)
            {
                Console.WriteLine("Error: " + ex.HResult + "\nError Message: " + ex.Message + "!");
            }
            //Return the dictonary data formated
            return _retval;

        }
#endif
            /// <summary>
            /// Export disk info to a file;
            /// </summary>
            /// <param name="ExportType">Type of file to be exported</param>
        public void ExportDiskLocalInfo(Constants.Constants.ExportType ExportType,Dictionary<string,string> source)
        {
            //Buffer to hold file
            byte[] _buffer = new byte[1024];          

            /*Using Serailization method*/

            //Binary Formater and Memory stream var
            var BinaryFormater = new BinaryFormatter();
            var mStream = new MemoryStream();
            //Try serialize with catch caught
            try
            {
                BinaryFormater.Serialize(mStream, source);
                _buffer = mStream.ToArray();
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            //Convert
            if (ExportType == Constants.Constants.ExportType.Text)
            {
                //Open File
                FileStream fs = new FileStream("C:\\_TEMP.log", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                try
                {   
                    fs.Write(_buffer, 0, 1024);
                }
                catch (IOException e)
                {
                    Console.WriteLine("Failed to Write to file. Reason: " + e.Message);
                    throw;
                }
                finally
                {
                    fs.Close();
                }
                 
            }        
        }
    }

}
