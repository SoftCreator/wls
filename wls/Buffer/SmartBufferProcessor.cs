using System.Collections.Generic;
using System.Text;
using wls.LogItems;

namespace wls.Buffer
{
    public class SmartBufferProcessor : IBufferProcessor
    {
        public string Process(IList<ILogItem> logItems)
        {
            var result = new StringBuilder();
            int i = 1;
            string prev = "";
            foreach (ILogItem logItem in logItems)
            {
                switch (logItem.ItemType)
                {
                    case LogItemType.AppTitle:
                        result.Append(i > 1 ? prev.Replace(">", string.Format(" {0}>", i)) : prev);
                        prev = "";
                        i = 1;

                        result.Append(string.Format("\r\n[{0}]\r\n", logItem.Value));
                        break;

                    case LogItemType.TimeStamp:
                        result.Append(string.Format("\r\n#{0}#\r\n", logItem.Value));
                        break;

                    default:
                        if (logItem.Value.Contains(">"))
                        {
                            if (prev == logItem.Value)
                                i++;
                            else
                            {
                                result.Append(i > 1 ? prev.Replace(">", string.Format(" {0}>", i)) : prev);
                                prev = logItem.Value;
                                i = 1;
                            }
                        }
                        else
                        {
                            result.Append(i > 1 ? prev.Replace(">", string.Format(" {0}>", i)) : prev);
                            result.Append(logItem.Value);
                            prev = "";
                            i = 1;
                        }
                        break;
                }
            }
            return result.ToString();
        }
    }
}
