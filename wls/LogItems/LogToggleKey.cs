using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wls.LogItems
{
    public class LogToggleKey : ILogItem
    {
        public LogToggleKey(string value)
        {
            Value = value;
        }

        public LogItemType ItemType { get { return LogItemType.ToggleKey; } }
        public string Value { get; private set; }
    }
}
