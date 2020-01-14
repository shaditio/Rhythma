using System.IO;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SongSelectionController : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject songSelectionsUI;
    public GameObject difficultyUI;
    public GameObject difficultyOptions;
    public GameObject settingsUI;
    public GameObject gameText;
    public GameObject songText;

    private bool hasEasy = false;
    private string easyBeatPath = "";
    private bool hasMedium = false;
    private string mediumBeatPath = "";
    private bool hasHard = false;
    private string hardBeatPath = "";

    void Start()
    {
        gameText.SetActive(true);
        songText.SetActive(false);
        generateSongSelections();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void BackToSongSelection()
    {
        difficultyUI.SetActive(false);
        songSelectionsUI.SetActive(true);
        songText.SetActive(false);
        gameText.SetActive(true);

        // Delete the difficulty button that's generated
        foreach (Transform child in difficultyOptions.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        clearSelection();
    }

    public void BackToDifficultyOptions()
    {
        settingsUI.SetActive(false);
        difficultyUI.SetActive(true);
    }

    private void getBeatMap()
    {
        DirectoryInfo currSongDir = new DirectoryInfo("Assets/Resources/Songs/" + GlobalStore.songTitlePicked);
        FileInfo[] beatMapFiles = currSongDir.GetFiles("*.JSON");

        Vector3 difficultyBtnLocalPos = new Vector3(0.0f, 0.0f, 0.0f);
        int levelSpacing = 0;
        float buttonSpacing = 35.0f;

        foreach (FileInfo beatMapFile in beatMapFiles)
        {
            // get the difficulty of the file
            string beatMapPath = beatMapFile.Name.Replace(beatMapFile.Extension, "");
            string[] beatMapSplit = beatMapPath.Split('_');
            string beatMapDifficultyLevel = beatMapSplit[1];

            if (beatMapDifficultyLevel.Equals("Easy"))
            {
                hasEasy = true;
                easyBeatPath = beatMapPath;
            }
            if (beatMapDifficultyLevel.Equals("Medium"))
            {
                hasMedium = true;
                mediumBeatPath = beatMapPath;
            }
            if (beatMapDifficultyLevel.Equals("Hard"))
            {
                hasHard = true;
                hardBeatPath = beatMapPath;
            }
        }

        if (hasEasy)
        {
            createDifficultyButton(easyBeatPath, "Easy", difficultyBtnLocalPos, levelSpacing, buttonSpacing);
            levelSpacing++;
        }
        if (hasMedium)
        {
            createDifficultyButton(mediumBeatPath, "Medium", difficultyBtnLocalPos, levelSpacing, buttonSpacing);
            levelSpacing++;
        }
        if (hasHard)
        {
            createDifficultyButton(hardBeatPath, "Hard", difficultyBtnLocalPos, levelSpacing, buttonSpacing);
        }
    }
    private void generateSongSelections()
    {
        DirectoryInfo songDir = new DirectoryInfo("Assets/Resources/Songs");
        DirectoryInfo[] songTitle = songDir.GetDirectories();

        for (int i = 0; i < songTitle.Length; i++)
        {
            GameObject go = GameObject.Instantiate<GameObject>(buttonPrefab);
            go.transform.SetParent(GameObject.FindGameObjectWithTag("SongList").transform);

            // assign listener manually
            Button b = go.GetComponent<Button>();

            string currTitle = songTitle[i].Name;

            b.GetComponentInChildren<Text>().text = currTitle;

            b.onClick.AddListener(() =>
            {
                GlobalStore.songTitlePicked = currTitle;

                songSelectionsUI.SetActive(false);

                // enable the difficulty UI
                difficultyUI.SetActive(true);
                songText.SetActive(true);
                gameText.SetActive(false);
                songText.GetComponent<Text>().text = currTitle;

                getBeatMap();
            });

        }
    }

    private void createDifficultyButton(string beatMapPath, string beatMapDifficultyLevel, Vector3 difficultyBtnLocalPos, int levelSpacing, float buttonSpacing)
    {
        GameObject go = GameObject.Instantiate<GameObject>(buttonPrefab);
        go.transform.SetParent(difficultyOptions.transform);

        // put spacing for each difficulty level button
        difficultyBtnLocalPos.y -= levelSpacing * buttonSpacing;
        go.transform.localPosition = difficultyBtnLocalPos;

        // Set the height and width
        go.GetComponent<RectTransform>().sizeDelta = new Vector3(0.0f, go.GetComponent<RectTransform>().sizeDelta[1]);

        // assign listener manually
        Button b = go.GetComponent<Button>();

        b.GetComponentInChildren<Text>().text = beatMapDifficultyLevel;

        b.onClick.AddListener(() =>
        {
            GlobalStore.beatMapDifficultyPicked = beatMapPath;

            // disable difficulty options
            difficultyUI.SetActive(false);

            // enable settings options
            settingsUI.SetActive(true);
        });
    }

    public void clearSelection(){
        hasHard = false;
        hasMedium = false;
        hasEasy = false;
        easyBeatPath = "";
        mediumBeatPath= "";
        hardBeatPath = "";
    }
}