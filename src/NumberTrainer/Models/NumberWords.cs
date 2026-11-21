namespace NumberTrainer.Models;

/// <summary>Spells 0-99 as words in English or Danish.</summary>
public static class NumberWords
{
    private static readonly string[] EnglishSmall =
    [
        "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine",
        "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"
    ];

    private static readonly string[] EnglishTens =
        ["", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"];

    private static readonly string[] DanishSmall =
    [
        "nul", "en", "to", "tre", "fire", "fem", "seks", "syv", "otte", "ni",
        "ti", "elleve", "tolv", "tretten", "fjorten", "femten", "seksten", "sytten", "atten", "nitten"
    ];

    private static readonly string[] DanishTens =
        ["", "", "tyve", "tredive", "fyrre", "halvtreds", "tres", "halvfjerds", "firs", "halvfems"];

    public static string ToWords(int number, Language language)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(number);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(number, 99);

        var (small, tens) = language == Language.Danish ? (DanishSmall, DanishTens) : (EnglishSmall, EnglishTens);
        if (number < 20)
            return small[number];

        int t = number / 10, o = number % 10;
        if (o == 0)
            return tens[t];

        // Danish says the ones first: 31 = "enogtredive" (one-and-thirty).
        return language == Language.Danish ? $"{small[o]}og{tens[t]}" : $"{tens[t]}-{small[o]}";
    }
}
