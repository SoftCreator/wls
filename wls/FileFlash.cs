using System.IO;
using wls.Buffer;

namespace wls
{
    public class FileFlash : IFlash
    {
        private readonly string _fileName;
        
        public FileFlash(string fileName)
        {
            _fileName = fileName;
        }

        public void Flash(LogBuffer buffer)
        {
            /*
            if (fileName.LastIndexOf('.') != -1)
            //file with extension
            {
                fileName = fileName.Insert(fileName.LastIndexOf('.'), "_" + Config.LogFileDatePattern);
            }
            else
                fileName = fileName + "_" + Config.LogFileDatePattern;
             */
            var fs = new FileStream(_fileName, FileMode.Append, FileAccess.Write);
            using (var sw = new StreamWriter(fs))
            {
                sw.Write(buffer.Flash());
            }
        }

    }
}
