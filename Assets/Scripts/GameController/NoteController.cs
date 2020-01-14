using UnityEngine;

public class NoteController : MonoBehaviour
{
    public Vector3 SpawnPos;
    public Vector3 RemovePos;
    public float beatsShownInAdvance;
    public float secondPerBeat;
    public float noteValue;
    public NoteGenerator noteGenerator;
    private Vector3 reflectedPosition = Vector3.zero;
    private Vector3 nextPosition;
    private float displacement;
    void Start()
    {
        if (noteGenerator == null)
        {
            this.noteGenerator = GameObject.FindGameObjectWithTag("NoteGenerator").GetComponent<NoteGenerator>();
        }
    }

    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (noteValue <= noteGenerator.songPositionBeats)
            {
                if (reflectedPosition == Vector3.zero)
                {
                    reflectedPosition = this.transform.position;
                }
                nextPosition = Vector3.Lerp(SpawnPos, RemovePos, Mathf.PingPong((beatsShownInAdvance - (noteValue - noteGenerator.songPositionBeats)) / beatsShownInAdvance, 1));
                displacement = Mathf.Abs(nextPosition.z - reflectedPosition.z);
                reflectedPosition = nextPosition;
                this.transform.position -= new Vector3(0, 0, displacement);
            }
            else
            {
                this.transform.position = Vector3.Lerp(SpawnPos, RemovePos, (beatsShownInAdvance - (noteValue - noteGenerator.songPositionBeats)) / beatsShownInAdvance);
            }
        }
    }
}
