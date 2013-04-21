namespace wls.LogItems
{
    public class LogKey : ILogItem
    {
        public LogKey(int code, int codePage, bool shift)
        {
            Value = KeyCodes.Instance.GetKey(code, codePage, shift);
        }

        public LogItemType ItemType { get { return LogItemType.Key; } }
        public string Value { get; private set; }
    }
}
