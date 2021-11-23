using OsmSharp.Tags;

namespace SkiAnalyze.Core.Util;

public static class TagExtensions
{
    public static string GetString(this TagsCollectionBase tags, string key)
    {
        if (tags.ContainsKey(key))
            return tags[key];

        return string.Empty;
    }
    public static string? GetNullableString(this TagsCollectionBase tags, string key)
    {
        if (tags.ContainsKey(key))
            return tags[key];

        return null;
    }
    public static int? GetNullableInt(this TagsCollectionBase tags, string key)
    {
        if (tags.ContainsKey(key))
        {
            if (int.TryParse(tags[key], out int value))
                return value;
        }
        return null;
    }

    public static bool? GetNullableBool(this TagsCollectionBase tags, string key)
    {
        if (tags.ContainsKey(key))
        {
            var val = tags[key];
            return val == "yes" || val == "true";
        }
        return null;
    }
}
