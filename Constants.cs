using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mainstream.Constants
{
    public class Constants
    {
        public enum Errors : long
        {
            //Error main class dll not load
            ERR_NATIVE_METHODS_NOT_LOADED = 0x0FF,

            //Mysql Commouns Errors
            MYSQL_DRIVER_NOT_INITIALIZED = 0x010,
            ERR_MYSQL_CONN_IS_ALREADY_OPEN = 0x11,
            

            //NetWorkErrors
            ERR_NETWORK_NOT_ACCEPTED = 0x40,
            ERR_NETWORK_NOT_PRESENT = 0x41,
            ERR_NETWORK_SOCKET_CLOSED = 0x42,
            ERR_NETWORK_SOCKET_CONNECTION_TIMEDOUT = 0x42

        }

        public enum Warnnings : long
        {
            W_MYSQL_CLOSE_CONN = 0x04,
            W_MYSQL_CONN_BROKE = 0x05
        }

        public enum ComandSelect : int
        {
            Select = 1,
            Update = 2,
            Insert = 3,
            Delete = 4
        }

        public enum CommandType : int
        {
            Image = 1,
            Blob = 2,
            Query = 3
        }

        public enum ExportType : int
        {
            Text = 0,
            XML = 1,
            CVS = 2,
            HTML = 3
        }

        public struct Convert
        {
            /// <summary>
            /// Convert an int to MegaByte value
            /// </summary>
            /// <param name="value">Input for conversion in byte form</param>
            /// <returns>Return an int with converted value</returns>
            public int ToMegaByte(int value)
            {
                if (value < (1024 * 1024))
                {
                    return value / (1024) / 1024;
                }

                return value / (1024);
            }
            public long ToMegaByte(long value)
            {
                if (value > (1024 * 1024))
                {
                    return value / (1024) / 1024;
                }
                return value / (1024);
            }

            public int ToGigaByte(int value) { return ToMegaByte(value) * 1024 ; }

        }
    }
}
