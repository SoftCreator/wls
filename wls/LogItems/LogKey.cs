namespace wls.LogItems
{
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
}
