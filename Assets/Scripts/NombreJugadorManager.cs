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
        botonJugar.interactable = false;
        inputNombre.onValueChanged.AddListener(VerificarNombre);

        // Restaurar nombre guardado si lo hay
        string nombreGuardado = PlayerPrefs.GetString("PlayerName", "");
        inputNombre.text = nombreGuardado;
        botonJugar.interactable = !string.IsNullOrWhiteSpace(nombreGuardado);
        nombreJugador = nombreGuardado;
    }

    void VerificarNombre(string valor)
    {
        botonJugar.interactable = !string.IsNullOrWhiteSpace(valor);

        if (!string.IsNullOrWhiteSpace(valor))
        {
            nombreJugador = valor;
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
