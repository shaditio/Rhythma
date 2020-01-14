using UnityEngine;
using UnityEngine.Events;

public class NoteGenerator : MonoBehaviour
{
    public GameObject noteTemplate;
    public GameObject skyNoteTemplate;
    public GameObject earthRocketTemplate;
    public GameObject skyRocketTemplate;
    public UnityEvent toSkyEvent;
    public UnityEvent toEarthEvent;
    public float songPositionBeats;
    private float beatsShownInAdvance;
    private float songPosition;
    private float secondPerBeat;
    private float dspTimeSong;
    private float bpm;
    private float firstBeatOffset;
    private NoteInfo noteInfo;
    private AudioSource music;

    // Put music aspect here instead of making a new Music Manager class to make the note and music synchronized
    public void loadMusic(string musicFileName){
        AudioClip clip = Resources.Load<AudioClip>(musicFileName);
        music = this.gameObject.AddComponent<AudioSource>();
        music.clip = clip;
        music.time = GlobalStore.songOffset;
        music.volume = GlobalStore.songVolume;
    }
    public void playMusic()
    {
        music.PlayDelayed(GlobalStore.songDelay);
    }

    public bool musicEnd(){
        if(!music.isPlaying){
            return true;
        }
        return false;
    }

    public void pauseMusic(){
        music.Pause();
    }

    void Update()
    {
        //calculate the position in seconds
        songPosition = (float)(AudioSettings.dspTime - dspTimeSong - firstBeatOffset + GlobalStore.songOffset);
        //calculate the position in beats
        songPositionBeats = songPosition / secondPerBeat;

        while (noteInfo.note.Count > 0 && noteInfo.note[0].beat < songPositionBeats + beatsShownInAdvance)
        {
            GameObject note;
            if (noteInfo.note[0].coordinate.y < 10)
            {
                note = GameObject.Instantiate<GameObject>(noteTemplate);
            }
            else
            {
                note = GameObject.Instantiate<GameObject>(skyNoteTemplate);
            }
            note.transform.parent = this.transform;
            note.transform.localPosition = noteInfo.note[0].coordinate.toVector3();

            NoteController noteController = note.GetComponent<NoteController>();
            noteController.SpawnPos = new Vector3(note.transform.localPosition.x, note.transform.localPosition.y + 0.01f, note.transform.localPosition.z);
            noteController.RemovePos = new Vector3(note.transform.localPosition.x, note.transform.localPosition.y + 0.01f, 1.5f);
            noteController.beatsShownInAdvance = beatsShownInAdvance;
            noteController.secondPerBeat = secondPerBeat;
            noteController.noteValue = noteInfo.note[0].beat;

            // remove the first note
            noteInfo.note.RemoveAt(0);
        }

        while (noteInfo.rocket.Count > 0 && noteInfo.rocket[0].beat < songPositionBeats + beatsShownInAdvance)
        {
            GameObject rocket = null;
            if (noteInfo.rocket[0].coordinate.y < 10)
            {
                rocket = GameObject.Instantiate<GameObject>(earthRocketTemplate);
            }
            else
            {
                rocket = GameObject.Instantiate<GameObject>(skyRocketTemplate);
            }

            rocket.transform.parent = this.transform;
            rocket.transform.localPosition = noteInfo.rocket[0].coordinate.toVector3();

            RocketController rocketController = rocket.GetComponent<RocketController>();
            rocketController.SpawnPos = new Vector3(rocket.transform.localPosition.x, rocket.transform.localPosition.y, rocket.transform.localPosition.z);
            rocketController.RemovePos = new Vector3(rocket.transform.localPosition.x, rocket.transform.localPosition.y, 1.5f);
            rocketController.beatsShownInAdvance = beatsShownInAdvance;
            rocketController.secondPerBeat = secondPerBeat;
            rocketController.noteValue = noteInfo.rocket[0].beat;

            // remove the first note
            noteInfo.rocket.RemoveAt(0);
        }

        if (noteInfo.transition.toEarth.Count > 0 && noteInfo.transition.toEarth[0] <= songPositionBeats){
            toEarthEvent.Invoke();
            noteInfo.transition.toEarth.RemoveAt(0);
        }

        if (noteInfo.transition.toSky.Count > 0 && noteInfo.transition.toSky[0] <= songPositionBeats){
            toSkyEvent.Invoke();
            noteInfo.transition.toSky.RemoveAt(0);
        }
    }

    // Generate notes from JSON file and return the number of notes for the song
    public int generateFromBeatmap(string fileName, float beatsShownInAdvance, float songDelay)
    {
        // Read the json from the file into a text asset
        TextAsset textAsset = Resources.Load<TextAsset>(fileName);
        if (textAsset != null)
        {
            // Pass the json to JsonUtility, and tell it to create a GameData object from it
            noteInfo = JsonUtility.FromJson<NoteInfo>(textAsset.text);
        }
        else
        {
            Debug.LogError("Beatmap not found!");
        }

        bpm = noteInfo.bpm;
        secondPerBeat = 60f / bpm;
        
        // Load and play the song here to reduce delay which can cause off sync problem
        loadMusic("Songs/" + GlobalStore.songTitlePicked + "/" + GlobalStore.songTitlePicked);
        dspTimeSong = (float)AudioSettings.dspTime;
        playMusic();
        
        firstBeatOffset = noteInfo.firstBeatOffset + songDelay + beatsShownInAdvance * noteInfo.beatShownMultiplier;
        this.beatsShownInAdvance = beatsShownInAdvance;

        return noteInfo.note.Count;
    }
}
