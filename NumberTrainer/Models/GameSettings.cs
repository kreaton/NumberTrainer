namespace NumberTrainer.Models;

public enum Language { English, Danish }

public enum Level { Easy, Medium, Hard }

public enum GameMode
{
    /// <summary>Show the number, wait, then play the sound and show the word.</summary>
    NumberThenSound,

    /// <summary>Play the sound, wait, then show the number and spell the word.</summary>
    SoundThenNumber
}

/// <summary>Which part of the number a clip speaks, so the UI can highlight it.</summary>
public enum SoundPart { Tens, Ones, Whole }

public sealed class GameSettings
{
    public Language Language { get; set; } = Language.Danish;
    public Level Level { get; set; } = Level.Easy;
    public GameMode Mode { get; set; } = GameMode.NumberThenSound;
    public int DelaySeconds { get; set; } = 3;

    public (int Min, int Max) Range => Level switch
    {
        Level.Easy => (1, 10),
        Level.Medium => (1, 30),
        _ => (0, 99)
    };

    /// <summary>On easy and medium a two-digit number is read digit by digit first: 31 = "three - one - thirty-one".</summary>
    public IReadOnlyList<(int Clip, SoundPart Part)> SoundSequence(int number) =>
        Level != Level.Hard && number >= 10
            ? [(number / 10, SoundPart.Tens), (number % 10, SoundPart.Ones), (number, SoundPart.Whole)]
            : [(number, SoundPart.Whole)];

    public string SoundUrl(int number) =>
        $"sounds/{(Language == Language.Danish ? "danish" : "english")}/speech{number}.mp3";
}
