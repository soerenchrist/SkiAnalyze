namespace SkiAnalyze.ApiModels.Stats;

public class BaseStatValueDto<TKey, TValue>
{
    public TKey Key { get; }
    public TValue Value { get; }

    public BaseStatValueDto(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }
}
