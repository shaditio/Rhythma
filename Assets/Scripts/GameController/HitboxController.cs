using UnityEngine;

public class HitboxController : MonoBehaviour
{
    public GameObject tapEffectA;
    public GameObject tapEffectB;
    public AudioClip tapSound;
    public ScoreManager scoreManager;
    public ComboManager comboManager;
    public KeyCode keyToPress;
    public bool canTrigger = false;
    public GameObject note;
    private AudioSource sfx;

    void Start()
    {
        // Find score manager by tag, if not referenced already
        if (scoreManager == null)
        {
            this.scoreManager = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<ScoreManager>();
        }

        // Also find combo manager by tag, if not referenced already
        if (comboManager == null)
        {
            this.comboManager = GameObject.FindGameObjectWithTag("ComboManager").GetComponent<ComboManager>();
        }
        sfx = gameObject.GetComponent<AudioSource>();
        sfx.clip = tapSound;
        sfx.volume = GlobalStore.sfxVolume;
    }
    void Update()
    {
        if ((this.transform.position.y < 10 && Camera.main.transform.position.y < 10) || (this.transform.position.y > 0 && Camera.main.transform.position.y > 10))
        {
            if (Input.GetKeyDown(keyToPress))
            {
                sfx.Play();
                GameObject tapEffectA = Instantiate(this.tapEffectA);
                tapEffectA.transform.position = this.transform.position;
                GameObject tapEffectB = Instantiate(this.tapEffectB);
                tapEffectB.transform.position = this.transform.position;
                if (canTrigger)
                {
                    Destroy(note);
                    canTrigger = false;
                    this.comboManager.incrementCombo();
                    this.scoreManager.scoreJudgement(note.transform.position, this.transform.position);
                }
            }
        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Note")
        {
            canTrigger = true;
            note = col.gameObject;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Note")
        {
            this.scoreManager.notifyMiss();
            canTrigger = false;
            this.comboManager.resetCombo();
        }
    }
}
