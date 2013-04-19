namespace wls.LogItems
{
    public class LogAppTitle : ILogItem
    {
        public LogAppTitle(string value)
        {
            Value = value;
        }

        public LogItemType ItemType { get { return LogItemType.AppTitle; } }
        public string Value { get; private set; }
    }
}
