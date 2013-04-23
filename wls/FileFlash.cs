using System;
using System.IO;
using wls.Buffer;

namespace wls
{
    public class FileFlash : IFlash
    {
        private readonly string _fileName;
        private readonly string _datePattern;
        
        public FileFlash(string fileName, string datePattern)
        {
            _fileName = fileName;
            _datePattern = datePattern;
        }

        public void Flash(LogBuffer buffer)
        {
            string fileName = _fileName;
            if (fileName.LastIndexOf('.') != -1)
            //file with extension
            {
                fileName = fileName.Insert(fileName.LastIndexOf('.'), "_" + DateTime.Now.ToString(_datePattern));
            }
            else
                fileName = fileName + "_" + _datePattern;

            var fs = new FileStream(fileName, FileMode.Append, FileAccess.Write);
            using (var sw = new StreamWriter(fs))
            {
                sw.Write(buffer.Flash());
            }
        }

    }
}
