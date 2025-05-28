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
        SceneManager.LoadScene("Level 1"); // o el nivel que corresponda
    }
}
