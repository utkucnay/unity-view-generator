public static class StringExtension
{
    public static string FirstCharacterToLower(this string s)
    {
        if (string.IsNullOrEmpty(s) || char.IsLower(s, 0))
        {
            return s;
        }

        return char.ToLowerInvariant(s[0]) + s.Substring(1);
    }

    public static string FirstCharacterToUpper(this string s)
    {
        if (string.IsNullOrEmpty(s) || char.IsUpper(s, 0))
        {
            return s;
        }

        return char.ToUpperInvariant(s[0]) + s.Substring(1);
    }
}
