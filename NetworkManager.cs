using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mainstream;
using Mainstream.Constants;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace Mainstream.NetworkManager
{
    public class NetworkManager
    {
        //Pointer to MessageBox IDE
        IntPtr hWnd;
        Mainstream.NativeMethods nm;  
        //Ctor
        public NetworkManager()
        {
            hWnd = new IntPtr(0);
            nm = new Mainstream.NativeMethods();
        }
        /// <summary>
        /// Capture and error and handle his ocurrence
        /// </summary>
        /// <param name="ErrorType"></param>
        /// <param name="caption"></param>
        public void ErrorHandling(Constants.Constants.Errors ErrorType, string caption)
        {

            Mainstream.NativeMethods.MessageBox(hWnd, caption + "\n" + ErrorType, "An Error has ocurred", 0);
        }

        /// <summary>
        /// Capture an error and handle his ocurrence by seding message to deferied email
        /// </summary>
        /// <param name="ErrorType"></param>
        /// <param name="caption">A error caption message</param>
        /// <param name="Email">Email address to send error mensage </param>
        public async void ErrorHandling(Constants.Constants.Errors ErrorType, string caption, string Email)
        {
            if (ErrorType == Constants.Constants.Errors.ERR_NETWORK_NOT_PRESENT)
            {
                Mainstream.NativeMethods.MessageBox(hWnd, "Host not present or accesible" + "\n" + ErrorType, "An Error has ocurred", 0);
                
                await nm.SendMessageThreadSafe("Host not present or accesible", Email, 587, 2321);
            }
        }
    }

    /// <summary>
    /// NetWork Manager Class with Service and other test.
    /// </summary>
    public class Services : NetworkManager
    {
        /// <summary>
        /// Check the host´s service 
        /// </summary>
        /// <param name="host">Host for check</param>
        /// <param name="port">Port to be checked</param>
        /// <param name="ProtocolType">Port protocol type TCP, UDP etc</param>
        /// <param name="ShowMessageBox">Show Message box for error handling</param>
        /// <returns>True if service pass, False if not</returns>
        public bool CheckServices(string host, int port, ProtocolType ProtocolType, bool ShowMessageBox)
        {
            //Instantiate sockte 
            var socket = new Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, ProtocolType);
            //Try connect with catch point
            try
            {

                socket.Connect(host, port);

                if (socket.Connected)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SocketException Ex)
            {
                if (ShowMessageBox == true)
                {
                    if (Ex.SocketErrorCode == SocketError.TimedOut)
                    {
                        this.ErrorHandling(Constants.Constants.Errors.ERR_NETWORK_SOCKET_CONNECTION_TIMEDOUT, "The port " + port + " was not responsing, or get timed out");

                    }
                    if (Ex.SocketErrorCode == SocketError.ConnectionRefused)
                    {
                        this.ErrorHandling(Constants.Constants.Errors.ERR_NETWORK_NOT_ACCEPTED, "The port " + port + " was not responsing, or get timed out");
                    }
                }

                return false;
            }
        }
        /// <summary>
        /// Check if Host is alive, if not return message.
        /// </summary>
        /// <param name="host"></param>
        /// <returns>True for pinging host.</returns>
        public bool IsHostAlive(string host)
        {
            Ping ping = new Ping();
            try
            {
                PingReply reply = ping.Send(host);
                if (reply.Status == IPStatus.Success)
                {
#if DEBUG
                    System.Threading.Thread.Sleep(1000);
#endif
                    return true;

                }
                else
                {
                    ErrorHandling(Constants.Constants.Errors.ERR_NETWORK_NOT_PRESENT, "Unable to ping");
                    return false;
                }
            }
            catch (PingException ex)
            {
                
                ErrorHandling(Constants.Constants.Errors.ERR_NETWORK_NOT_PRESENT,ex.ToString());
                return false;
            }
        }
    }

}
