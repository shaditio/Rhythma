using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public void goToSongSelection(){
		SceneManager.LoadScene("SongSelection");
    }

    public void retry(){
		SceneManager.LoadScene("Game");
    }
}