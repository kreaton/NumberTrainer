# Number Trainer / Taltræner

![Blazor WebAssembly](https://img.shields.io/badge/Blazor-WebAssembly-lightgrey)
![.NET 10](https://img.shields.io/badge/.NET-10-lightgrey)
![Installable web app](https://img.shields.io/badge/PWA-installable-lightgrey)
[![License: MIT](https://img.shields.io/badge/license-MIT-blue)](LICENSE)
[![GitHub stars](https://img.shields.io/github/stars/kreaton/NumberTrainer?style=flat&logo=github&color=yellow)](https://github.com/kreaton/NumberTrainer/stargazers)

⭐ If Number Trainer earns a place on your machine, [star it on GitHub](https://github.com/kreaton/NumberTrainer) — it's free, made by one developer, and stars are how other people find it.

![Number Trainer: learn the numbers 0-99 in Danish or English](images/banner.png)

**A small game for children learning the numbers 0–99 in Danish or English, using recorded number sounds.**

▶️ **Play it:** <https://kreaton.github.io/KidsGames/numbertrainer/>

## How to play

1. **Pick a language:** 🇩🇰 Dansk or 🇬🇧 Engelsk. This picks the number sounds and spelled words; the app's own texts are always Danish.
2. **Pick a level**
   | Level | Icon | Numbers |
   |---|---|---|
   | Let (easy) | ⭐ | 1–10 |
   | Mellem (medium) | ⭐⭐ | 1–30 |
   | Svær (hard) | ⭐⭐⭐ | 0–99 |
3. **Pick a game**
   - **🔢➜🔊 Se, så hør (see, then hear):** the number is shown. After the thinking time the sound plays and the word appears.
   - **🔊➜🔢 Hør, så se (hear, then see):** the sound plays. After the thinking time the number appears and the word is spelled letter by letter.
4. **Set the thinking time** (1–10 seconds, default 3). During it the card shows 🗣️ *"Sig tallet højt!"* (say the number out loud): the child should say the number before the answer comes.
5. **🔁 Igen** (again) repeats the sound. **➡️ Næste** (next) goes to a new random number, even in the middle of a sound. **🏠** goes back to the start screen.

Each number is read once, and it lights up while it is spoken. The same number never comes twice in a row.

## iPhone and iPad

Open the site in Safari and use **Share → Add to Home Screen**. It then opens full screen as **Taltræner**, with its own icon (the Danish and British flags with a number).

- iOS only allows sound after a tap, so the app primes its audio on the first tap anywhere on the page.
- On iOS 17 and later, sound plays even when the silent switch is on. On older iOS, turn the silent switch off.

### How clips are held in memory

When a game starts, `audio.js` downloads every MP3 for the level so playback starts instantly. The files on the server are plain MP3s and never change; this is only about how the browser keeps them after downloading.

- **Now:** each clip is turned into a `data:` URL, which is the whole sound written out as base64 text, and that is given to the audio player.
- **Before:** each clip was turned into a `blob:` URL, a short temporary address pointing at the downloaded bytes.

**Why:** Safari plays audio from a `blob:` address differently from a normal file, and Safari on iOS has a history of bugs where such audio fails to load or play. A `data:` URL contains the sound itself, so that can't happen. This is a precaution, not a reproduced bug; recent Safari versions usually handle `blob:` audio fine.

**Cost:** base64 takes about a third more memory than the MP3. On Hard all 100 clips are loaded: about 2.4 MB of Danish MP3s (≈3.2 MB in memory) or 1.9 MB of English (≈2.5 MB). That is fine on any current phone.

**To switch back,** change `.then(toDataUrl)` to `.then(b => URL.createObjectURL(b))` in `load()` in `wwwroot/js/audio.js`.

## Running it

Requires the .NET 10 SDK.

```bash
dotnet run --project src/NumberTrainer --launch-profile http
```

Then open http://localhost:5074. Or open `NumberTrainer.slnx` in Visual Studio and press F5.

To publish as static files (any static web host works):

```bash
dotnet publish src/NumberTrainer -c Release -o publish
```

The site is then in `publish/wwwroot`.

## Sound files

The recordings live in `src/NumberTrainer/wwwroot/sounds/<language>/speech<n>.mp3`, where `<language>` is `danish` or `english` and `<n>` is 0–99. To use your own recordings, replace those files with the same names.

The folders also contain a few Italian words (`stasera.mp3`, `venerdi.mp3`, …) that the app does not use.

## Project layout

The project is in `src/NumberTrainer/`; the paths below are relative to it.

| File | What it does |
|---|---|
| `Pages/Home.razor` | Start screen and game: rounds, thinking time, repeat/next, the Danish UI texts |
| `Models/GameSettings.cs` | Language, level, mode, delay; number ranges; sound file for a number |
| `Models/NumberWords.cs` | Spells 0–99 in English (*thirty-one*) and Danish (*enogtredive*) |
| `Services/AudioPlayer.cs` | C# wrapper around `wwwroot/js/audio.js` |
| `wwwroot/js/audio.js` | Preloads the level's clips, plays one clip at a time, unlocks sound on iOS |
| `wwwroot/icon.svg` | App icon source; the PNGs (`apple-touch-icon.png`, `icon-*.png`, `favicon-32.png`) are rendered from it |
| `wwwroot/manifest.webmanifest` | Name and icons for "Add to Home Screen" |
| `Components/Flag.razor` | Danish and British flags as inline SVG (flag emoji don't show on Windows) |
| `wwwroot/css/app.css` | Styling, including dark mode and reduced motion |

## Known limits

- Settings are not remembered between visits.
- The iOS handling follows Safari's rules but has not yet been tried on a real iPhone or iPad.

## License & Authorship

This project is distributed under the terms of the [MIT License](LICENSE).

**AI authorship note:** Most of the source code in this repository was generated with AI coding agents (Claude Code), under my direction and review. Purely AI-generated material may not be eligible for copyright protection in some jurisdictions. To the extent any part of this project is protected by copyright, it is licensed under the MIT License, and the license's warranty and liability disclaimers apply to the project as a whole.

<!-- notices:start -->
### Independent development

NumberTrainer is an independent, personal project developed by Per Hejndorf. It is not affiliated with, endorsed by or sponsored by Microsoft or any other rights holder (including ElevenLabs). Product names and logos are trademarks of their respective owners and are used only to identify the services and tools the project works with.

### Third-party notices and asset provenance

[THIRD-PARTY-NOTICES.md](THIRD-PARTY-NOTICES.md) lists every third-party library, font and online service the project uses, with version, licence, source and whether it was modified. It also records where the icons, images, audio and video came from and how they are licensed. A few assets are not covered by the MIT License; [THIRD-PARTY-NOTICES.md](THIRD-PARTY-NOTICES.md) marks them and states their terms.
<!-- notices:end -->
