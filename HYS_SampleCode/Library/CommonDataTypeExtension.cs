using System;

public static class CommonDataTypeExtension
{
    public static float ToFloat(this string t)
    {
        float.TryParse(t, out float v);
        return v;
    }

    public static int ToInt(this string t)
    {
        int.TryParse(t, out int v);
        return v;
    }

    public static uint ToUInt(this string t)
    {
        uint.TryParse(t, out uint v);
        return v;
    }

    public static bool ToBoolean(this string t)
    {
        bool.TryParse(t, out bool v);
        return v;
    }
}