using System.Collections.Generic;
using wls.LogItems;

namespace wls.Buffer
{
    public interface IBufferProcessor
    {
        string Process(IList<ILogItem> logItems);
    }
}
