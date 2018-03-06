using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Mainstream.Cryptography
{
    public class CEncryptSystem : IDisposable
    {
        //Variables
        public bool bIsComplete = false;
        public bool bIsWorking = false;
        public bool bIsConverting = false;

        public byte[] data;
        public byte[] salt;

        //Instances
        public MD5CryptoServiceProvider _MD5;
        public SHA1CryptoServiceProvider _SHA1;
        public UTF8Encoding utf8;
        public StringBuilder sBuilder;

        public class SHA1Encrypt : CEncryptSystem
        {
            public string GetSHA1Hash(string _input)
            {
                //Instantiate Class
                _SHA1 = new SHA1CryptoServiceProvider();
                utf8 = new UTF8Encoding();
                //Compute Hash
                data = _SHA1.ComputeHash(utf8.GetBytes(_input));
                //Hash to String(Formatex XXXX-XXX-XXX-XXX)
                sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                    bIsWorking = true;
                }
                bIsWorking = false;
                return sBuilder.ToString();
            }
            public static string GetFileSHA1HashSalted(string _finputm, byte[] salt)
            {

                throw new NotImplementedException("NOT IMPLEMENTED - WILL BE ADD ON VERSION 2.0");
            }
        }

        public class MD5_Encryptation : CEncryptSystem
        {
            /// <summary>
            /// Get MD5 Hash emcryptation Converted to String
            /// </summary>
            /// <param name="_input">String to be Converted</param>
            /// <returns>Return the string of encrypted Hash</returns>
            public string GetMD5Hash(string _input)
            {

                bIsComplete = false;
                //Instancing Objects
                _MD5 = new MD5CryptoServiceProvider();
                utf8 = new UTF8Encoding();
                sBuilder = new StringBuilder();
                //Seting Working
                bIsWorking = false;
                //Getting Data e insirting into a byte
                data = _MD5.ComputeHash(utf8.GetBytes(_input));
                //String builder to build your data
                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                    bIsWorking = true;
                }

                bIsWorking = false;
                bIsComplete = true;
                // Return the hexadecimal string.
                return sBuilder.ToString();
            }

        }

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
        ~CEncryptSystem()
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
                if (_MD5 != null) { _MD5.Dispose(); _MD5 = null; }
                if (sBuilder != null) { }

            }
        }
        #endregion

    }

    //public class CDecryptSystem : CEncryptSystem
    //  {
    //     TripleDESCryptoServiceProvider _Descrypto;

    //     public class SHA1_Decryptation : CDecryptSystem
    //     {
    //        public string SHADecrypt(string _output)
    //         {
    //           throw new NotImplementedException("NOT IMPLEMENTED - WILL BE ADD ON VERSION 2.0");
    ////
    //      }
    //  }
}

