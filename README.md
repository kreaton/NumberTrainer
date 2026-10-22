# Number Trainer / Taltræner

A small Blazor WebAssembly game for children learning the numbers 0–99 in **Danish** or **English**, using recorded number sounds.

## How to play

1. **Pick a language:** 🇩🇰 Dansk or 🇬🇧 English. The app's own texts switch language too.
2. **Pick a level**
   | Level | Icon | Numbers |
   |---|---|---|
   | Easy / Let | ⭐ | 1–10 |
   | Medium / Mellem | ⭐⭐ | 1–30 |
   | Hard / Svær | ⭐⭐⭐ | 0–99 |
3. **Pick a game**
   - **🔢➜🔊 See, then hear / Se, så hør:** the number is shown. After the thinking time the sound plays and the word appears.
   - **🔊➜🔢 Hear, then see / Hør, så se:** the sound plays. After the thinking time the number appears and the word is spelled letter by letter.
4. **Set the thinking time** (1–10 seconds, default 3). During it the card shows 🗣️ *"Sig tallet højt!" / "Say the number out loud!"*: the child should say the number before the answer comes.
5. **🔁 Again / Igen** repeats the sound. **➡️ Next / Næste** goes to a new random number, even in the middle of a sound. **🏠** goes back to the start screen.

On **easy** and **medium**, two-digit numbers are read digit by digit first, then as a whole, and each digit lights up as it is spoken:

- 31 in English: *three – one – thirty-one*
- 31 in Danish: *tre – en – enogtredive*

On **hard** only the whole number is read. The same number never comes twice in a row.

## Running it

Requires the .NET 10 SDK.

```bash
dotnet run --project NumberTrainer --launch-profile http
```

Then open http://localhost:5074. Or open `NumberTrainer.slnx` in Visual Studio and press F5.

To publish as static files (any static web host works):

```bash
dotnet publish NumberTrainer -c Release -o publish
```

The site is then in `publish/wwwroot`.

## Sound files

The recordings live in `NumberTrainer/wwwroot/sounds/<language>/speech<n>.mp3`, where `<language>` is `danish` or `english` and `<n>` is 0–99. To use your own recordings, replace those files with the same names.

The folders also contain a few Italian words (`stasera.mp3`, `venerdi.mp3`, …) that the app does not use.

## Project layout

| File | What it does |
|---|---|
| `Pages/Home.razor` | Start screen and game: rounds, thinking time, repeat/next, UI texts in both languages |
| `Models/GameSettings.cs` | Language, level, mode, delay; number ranges; which clips to play for a number |
| `Models/NumberWords.cs` | Spells 0–99 in English (*thirty-one*) and Danish (*enogtredive*) |
| `Services/AudioPlayer.cs` | C# wrapper around `wwwroot/js/audio.js` |
| `wwwroot/js/audio.js` | Preloads the level's clips and plays one clip at a time |
| `Components/Flag.razor` | Danish and British flags as inline SVG (flag emoji don't show on Windows) |
| `wwwroot/css/app.css` | Styling, including dark mode and reduced motion |

## Known limits

- Settings are not remembered between visits.
- Sound on iPhone/iPad has not been tested.
