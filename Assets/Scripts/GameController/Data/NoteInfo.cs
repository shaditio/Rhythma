using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoteInfo
{
    public List<Note> note;
    public List<Note> rocket;
    public Transition transition;
    public float firstBeatOffset;
    public float beatShownMultiplier;
    public float bpm;
    public static NoteInfo CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<NoteInfo>(jsonString);
    }
}