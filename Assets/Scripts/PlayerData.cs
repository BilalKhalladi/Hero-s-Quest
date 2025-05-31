using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerData : MonoBehaviour
{
    public TMP_InputField nameInput;

    public void StartGame()
    {
        string playerName = nameInput.text;
        if (string.IsNullOrEmpty(playerName)) return;

        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();

        Time.timeScale = 1f; 

        SceneManager.LoadScene("Level 1");

    }

}
