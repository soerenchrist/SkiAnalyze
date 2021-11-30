namespace SkiAnalyze.Core.Common;

public class BaseStatValue<TKey, TValue>
{
    public TKey Key { get; }
    public TValue Value { get; }

    public BaseStatValue(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }
}