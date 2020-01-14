using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionController : MonoBehaviour
{
    public GameObject firstPage;
    public GameObject secondPage;
    public GameObject thirdPage;
    public GameObject fourthPage;
    public GameObject fifthPage;
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void goToFirstPage()
    {
        firstPage.SetActive(true);
        secondPage.SetActive(false);
    }

    public void goToSecondPage()
    {
        secondPage.SetActive(true);
        firstPage.SetActive(false);
        thirdPage.SetActive(false);
    }

    public void goToThirdPage()
    {
        thirdPage.SetActive(true);
        secondPage.SetActive(false);
        fourthPage.SetActive(false);
    }

    public void goToFourthPage()
    {
        fourthPage.SetActive(true);
        thirdPage.SetActive(false);
        fifthPage.SetActive(false);
    }

    public void goToFifthPage()
    {
        fifthPage.SetActive(true);
        fourthPage.SetActive(false);
    }
}