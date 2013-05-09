using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wls
{
    public class Config
    {
        public static string LogFileDatePattern
        {
            get { return ConfigurationManager.AppSettings["LogFileDatePattern"]; }
        }        
        public static string TimeStamp
        {
            get { return ConfigurationManager.AppSettings["TimeStamp"]; }
        }

        public static string LogFileName
        {
            get { return ConfigurationManager.AppSettings["LogFileName"]; }
        }

        public static int FlashInterval
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["FlashInterval"]); // 10 seconds
            }
        }
    }
}
