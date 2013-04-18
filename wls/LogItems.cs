namespace wls
{
    public enum LogItemType
    {
        Key,
        ToggleKey,
        AppTitle
    }

    public interface ILogItem
    {
        LogItemType ItemType { get; }
        string Value { get; }
    }

    public class LogKey : ILogItem
    {
        public LogKey(int code, int codePage, bool upper)
        {
            Value = KeyCodes.Instance.GetKey(code, codePage);
            if (upper) Value = Value.ToUpperInvariant();
        }

        public LogItemType ItemType { get { return LogItemType.Key; } }
        public string Value { get; private set; }
    }

    public class LogToggleKey : ILogItem
    {
        public LogToggleKey(string value)
        {
            Value = value;
        }

        public LogItemType ItemType { get { return LogItemType.ToggleKey; } }
        public string Value { get; private set; }
    }

    public class LogAppTitle : ILogItem
    {
        public LogAppTitle(string value)
        {
            Value = value;
        }

        public LogItemType ItemType { get { return LogItemType.ToggleKey; } }
        public string Value { get; private set; }
    }

}
