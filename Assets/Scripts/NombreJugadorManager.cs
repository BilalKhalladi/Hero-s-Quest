using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NombreJugadorManager : MonoBehaviour
{
    public TMP_InputField inputNombre;
    public Button botonJugar;

    public static string nombreJugador = "";

    void Start()
    {
        botonJugar.interactable = false;
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
}

