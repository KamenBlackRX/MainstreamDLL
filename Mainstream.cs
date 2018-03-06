using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;

namespace Mainstream
{
    public class Mainstream
    {
        /// <summary>
        /// Call Win32 user.dll Functions
        /// </summary>
        public partial class NativeMethods : Mainstream
        {

            /// <summary>
            ///Open a Message Box using Win32 user32.dll function
            /// </summary>
            /// <param name="hWnd">Int Pointer to Windows handle </param>
            /// <param name = "text">Text to message title </param>
            /// // Use DllImport to import the Win32 MessageBox function.
             [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Interoperability", "CA1401:PInvokesShouldNotBeVisible"), DllImport("user32.dll", CharSet = CharSet.Unicode)]
            public static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);

            /// <summary>
            /// Send message with error included
            /// </summary>
            /// <param name="message">The caption message to be mailed</param>
            /// <param name="host">The host to be connected</param>
            /// <param name="errcode">Error code indentification</param>
            /// <returns>Send message with error</returns>
            public async Task SendMessageThreadSafe(string message, string smtphost,int port, long errcode)
            {
                try
                {
                    System.Net.Mail.SmtpClient smtpclient = new System.Net.Mail.SmtpClient(smtphost, port);
                    smtpclient.Port = 587;
                    smtpclient.Host = "smtp.gmail.com";
                    smtpclient.EnableSsl = true;
                    smtpclient.Timeout = 10000;
                    smtpclient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                    smtpclient.UseDefaultCredentials = false;
                    smtpclient.Credentials = new System.Net.NetworkCredential("puchiruzuki", "ac/dc03[]");

                    System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage("puchiruzuki@gmail.com", "puchiruzuki@gmail.com", "Mainstream UMS Bug track system", "Bug track number: " + errcode + ".\n" +  message);

                    smtpclient.Send(mm);

                    MessageBox(new IntPtr(0), "Bug Registrado com sucesso!\n Bug error track " + errcode + ".", message ,0);
                    //Free resources
                    smtpclient.Dispose();

                }
                catch (Exception ex)
                {
                    MessageBox(new IntPtr(0), "Send message error", ex.Message, 0);
                    throw;
                }
                 
            }
            
        }

        public void Dispose()
        {
        }
            
    }
}
