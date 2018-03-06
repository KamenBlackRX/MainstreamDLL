 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace Mainstream.Compression
{
    /// <summary>
    /// Generic main File class
    /// </summary>

    public class FileCompress
    {
        public string _openfilepath { get; set; }
        public string _directory_path { get; set; }
        public string[] _fileinfo { get; set; }
    }
    public class FileCompressZip : FileCompress
    {
        /// <summary>
        /// Main Entry Class for File Compress mode ZIP
        /// </summary>
        public FileCompressZip(string __filepath)
        {
            this._openfilepath = __filepath;
        }

        public FileCompressZip(string __filepath, string __directoryPath)
        {
            this._openfilepath = __filepath;
            this._directory_path = __directoryPath;
        }


        public void Compress()
        {
            ZipFile.CreateFromDirectory(_openfilepath,_directory_path);
        }

        public void Decompress()
        {
            ZipFile.ExtractToDirectory(_openfilepath, _directory_path);
        }
    }
    public class FileCompressGzip : FileCompress
    {
        /// <summary>
        /// Main entry Class for File compress mode in GZIP
        /// <param name="__filepath">Path to open file to compress </param>
        /// </summary>
        public FileCompressGzip(string __filepath)
        {
            this._openfilepath = __filepath;
        }
        /// <summary>
        /// Get Raw Data, and compress to GZIP
        /// </summary>
        /// <param name="_fileName">Name of File to compress</param>
        public void Compress(string _fileName)
        {
            //New Fileinfo Class wrapper
            FileInfo finfo = new FileInfo(_fileName);
            //using Filestream
            using (FileStream fs = finfo.OpenRead())
            {
                //Loop to get File ext
                if ((File.GetAttributes(finfo.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & finfo.Extension != ".gz")
                {
                    //Open File stream to create compress file
                    using (FileStream compressedFileStream = File.Create(finfo.FullName + ".gz"))
                    {
                        //Using Gzip filestream, with compress mode
                        using (GZipStream Gzip_s = new GZipStream(compressedFileStream, CompressionMode.Compress))
                        {

                            fs.CopyTo(Gzip_s);
                            Console.WriteLine("Compressed {0} from {1} to {2} bytes.", finfo.Name, finfo.Length.ToString(), compressedFileStream.Length.ToString());
                            
                        }
                    }
                }
            }
        }

        public void Decompress(string _fileName)
        {
            //File winfo class wrapper
            FileInfo finfo = new FileInfo(_fileName);
            //Using filestrream to open file
            using (FileStream fs = finfo.OpenRead())
            {
                //Loop to get file extension
                if (((File.GetAttributes(finfo.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & finfo.Extension !=".gz"))
                {
                    //Using file stream to create a new file who hold a file Data decompressed.
                    using (FileStream decompressedFileStream = File.Create(finfo.FullName))
                    {
                        //Using gzipstream to decompress
                        using (GZipStream Gzip_ds = new GZipStream(decompressedFileStream,CompressionMode.Decompress))
                        {
                            Gzip_ds.CopyTo(decompressedFileStream);
                            Console.WriteLine("Decompressed: {0}", finfo.Name);
                        }
                    }
                }
            }
        }
    }
}
