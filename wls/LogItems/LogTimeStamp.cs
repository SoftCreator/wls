using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wls.LogItems
{
    public class LogTimeStamp : ILogItem
    {
        public LogTimeStamp(string value)
        {
            Value = value;
        }

        public LogItemType ItemType { get { return LogItemType.TimeStamp; } }
        public string Value { get; private set; }
    }
}
