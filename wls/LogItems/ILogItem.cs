namespace wls.LogItems
{
    public interface ILogItem
    {
        LogItemType ItemType { get; }
        string Value { get; }
    }
}
