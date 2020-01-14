using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameController : MonoBehaviour
{
    // InGameUI attribute
    public Text scoreText;
    public Slider healthValue;
    public Text comboText;
    public GameObject comboUI;
    public GameObject inGameUI;

    // Result UI attribute
    public GameObject resultUI;
    public Text scoreResult;
    public Text comboResult;
    public Text perfectHit;
    public Text greatHit;
    public Text goodHit;
    public Text badHit;
    public Text missHit;

    // Pause UI
    public GameObject pauseUI;

    // Manager and Generator
    public NoteGenerator noteGenerator;
    public ScoreManager scoreManager;
    public HealthManager healthManager;
    public MusicManager musicManager;
    public ComboManager comboManager;

    private bool paused = false;
    void Start()
    {
        this.scoreManager.score = 0;
        this.healthManager.health = 100;

        // Note Speed
        float beatsShownInAdvance = 11 - GlobalStore.speed;

        //  Need to follow this order of initialisation
        //int noteCount = noteGenerator.generateFromBeatmap("Songs/" + GlobalStore.songTitlePicked + "/" + GlobalStore.beatMapDifficultyPicked, beatsShownInAdvance, GlobalStore.songDelay);
        //musicManager.playMusic("Songs/" + GlobalStore.songTitlePicked + "/" + GlobalStore.songTitlePicked);

        // STEVEN'S TESTING CODE. DON'T DELETE FOR NOW!
         float canonBeatsShownInAdvance = 5f;
         int noteCount = noteGenerator.generateFromBeatmap("Songs/Pachelbel CANON (60 BPM) Piano Accompaniment/canon_hard", canonBeatsShownInAdvance, GlobalStore.songDelay);
         musicManager.playMusic("Songs/Pachelbel CANON (60 BPM) Piano Accompaniment/Pachelbel CANON (60 BPM) Piano Accompaniment");

        // Set the noteCount for score manager to calculate relative score
        scoreManager.noteCount = noteCount;

        resultUI.SetActive(false);
    }

    void Update()
    {
        // Update score text field
        this.scoreText.text = ((int)this.scoreManager.score).ToString();
        // Update health slider
        this.healthValue.value = this.healthManager.health;

        // Show combo if the combo is higher than 5
        if (this.comboManager.combo >= 5 && !comboUI.activeSelf)
        {
            this.comboUI.SetActive(true);
        }
        if (this.comboManager.combo < 5 && comboUI.activeSelf)
        {
            this.comboUI.SetActive(false);
        }

        if (comboUI.activeSelf)
        {
            comboText.text = this.comboManager.combo.ToString();
        }

        // Show the result if the music has ended
        if (musicManager.musicEnd() && !paused)
        {
            // The song has end, hide in game UI
            inGameUI.SetActive(false);

            // fill in the detail of result UI
            resultUI.SetActive(true);
            scoreResult.text = $"Score : {(int)this.scoreManager.score}";
            comboResult.text = $"Max Combo : {this.comboManager.maxCombo}";
            perfectHit.text = $"{this.scoreManager.perfectHit}";
            greatHit.text = $"{this.scoreManager.greatHit}";
            goodHit.text = $"{this.scoreManager.goodHit}";
            badHit.text = $"{this.scoreManager.badHit}";
            missHit.text = $"{this.scoreManager.getMissHit()}";
        }

        if (this.healthManager.health <= 0)
        {
            // Show the fail scene to go back to main menu or song selection menu
            SceneManager.LoadScene("GameOver");
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !resultUI.activeSelf)
        {
            if (Time.timeScale == 0)
            {
                unPauseGame();
            }
            else
            {
                pauseGame();
            }
        }
    }

    public void returnToSongSelection()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        SceneManager.LoadScene("SongSelection");
    }

    public void retry()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        SceneManager.LoadScene("Game");
    }

    public void pauseGame()
    {
        paused = true;
        pauseUI.SetActive(true);
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public void unPauseGame()
    {
        paused = false;
        pauseUI.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause = false;
    }
}

