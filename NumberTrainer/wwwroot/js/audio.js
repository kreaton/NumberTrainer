// One shared <audio> element; clips are fetched once and kept as blob URLs so playback starts instantly.
const audio = new Audio();
const clips = new Map();
let pending = null;

function load(url) {
    let clip = clips.get(url);
    if (!clip) {
        clip = fetch(url)
            .then(r => {
                if (!r.ok) throw new Error(`${r.status} ${url}`);
                return r.blob();
            })
            .then(b => URL.createObjectURL(b))
            .catch(e => {
                clips.delete(url);
                throw e;
            });
        clips.set(url, clip);
    }
    return clip;
}

export function preload(urls) {
    for (const url of urls) load(url).catch(() => { });
}

export function play(url) {
    stop();
    return new Promise(resolve => {
        const finish = ok => {
            if (pending !== token) return;
            pending = null;
            audio.onended = audio.onerror = null;
            resolve(ok);
        };
        const token = { finish };
        pending = token;
        audio.onended = () => finish(true);
        audio.onerror = () => finish(false);
        load(url)
            .then(src => {
                if (pending !== token) return;
                audio.src = src;
                return audio.play();
            })
            .catch(e => {
                if (pending !== token) return; // stopped while starting; not an error
                console.warn('Could not play', url, e);
                finish(false);
            });
    });
}

export function stop() {
    audio.pause();
    pending?.finish(false);
}
