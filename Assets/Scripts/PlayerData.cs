using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

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

    [System.Serializable]
    public class MarcaJugador
    {
        public string nombre;
        public float tiempo;
        public int monedas;
        public float distancia;

        public MarcaJugador(string nombre, float tiempo, int monedas, float distancia)
        {
            this.nombre = nombre;
            this.tiempo = tiempo;
            this.monedas = monedas;
            this.distancia = distancia;
        }
    }

    [System.Serializable]
    public class MarcaJugadorLista
    {
        public List<MarcaJugador> marcas = new List<MarcaJugador>();
    }

}
