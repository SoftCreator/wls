using System.Collections.Generic;
using System.Text;
using wls.LogItems;

namespace wls.Buffer
{
    public class PlainBufferProcessor : IBufferProcessor
    {
        public string Process(IList<ILogItem> logItems)
        {
            var result = new StringBuilder();
            foreach (ILogItem logItem in logItems)
            {
                switch (logItem.ItemType)
                {
                    case LogItemType.AppTitle:
                        result.Append(string.Format("\r\n[{0}]\r\n", logItem.Value));
                        break;
                    default:
                        result.Append(logItem.Value);
                        break;
                }
            }
            return result.ToString();
        }
    }
}
