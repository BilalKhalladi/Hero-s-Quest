using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class NombreJugadorManager : MonoBehaviour
{
    public TMP_InputField inputNombre;
    public Button botonJugar;

    public static string nombreJugador = "";

    void Start()
    {
        string guardado = PlayerPrefs.GetString("PlayerName", "");
        inputNombre.text = guardado;
        botonJugar.interactable = !string.IsNullOrWhiteSpace(guardado);
        inputNombre.onValueChanged.AddListener(VerificarNombre);
    }


    void VerificarNombre(string texto)
    {
        bool valido = !string.IsNullOrWhiteSpace(texto);
        botonJugar.interactable = valido;

        if (valido)
        {
            nombreJugador = texto;
        }
    }

    public void IniciarJuego(string nombreEscena)
    {
        if (string.IsNullOrWhiteSpace(inputNombre.text)) return;

        nombreJugador = inputNombre.text;
        PlayerPrefs.SetString("PlayerName", nombreJugador);
        PlayerPrefs.Save();

        Time.timeScale = 1f;

        SceneManager.LoadScene(nombreEscena);
    }

}

