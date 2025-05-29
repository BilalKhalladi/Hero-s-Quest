using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Salir del juego");
    }

    public void RankingMenu()
    {
        SceneManager.LoadScene("RankingMenu");
    }

    public void BackMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

