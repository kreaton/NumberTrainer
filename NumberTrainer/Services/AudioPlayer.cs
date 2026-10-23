using Microsoft.JSInterop;

namespace NumberTrainer.Services;

/// <summary>Plays one clip at a time through wwwroot/js/audio.js.</summary>
public sealed class AudioPlayer(IJSRuntime js) : IAsyncDisposable
{
    private Task<IJSObjectReference>? _module;

    private Task<IJSObjectReference> Module =>
        _module ??= js.InvokeAsync<IJSObjectReference>("import", "./js/audio.js").AsTask();

    /// <summary>Loads the script early so it can unlock audio on the first tap (required on iOS).</summary>
    public async Task InitAsync() => await Module;

    /// <summary>Plays a clip and completes when it ends. Returns false if it was stopped or failed.</summary>
    public async Task<bool> PlayAsync(string url) =>
        await (await Module).InvokeAsync<bool>("play", url);

    public async Task StopAsync() =>
        await (await Module).InvokeVoidAsync("stop");

    public async Task PreloadAsync(IEnumerable<string> urls) =>
        await (await Module).InvokeVoidAsync("preload", urls);

    public async ValueTask DisposeAsync()
    {
        if (_module is null)
            return;
        try
        {
            await (await _module).DisposeAsync();
        }
        catch (JSDisconnectedException)
        {
        }
    }
}
