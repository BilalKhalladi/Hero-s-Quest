using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI distanceText;
    public TextMeshProUGUI timeText;

    private int coins = 0;
    private float distance = 0f;
    private float timer = 0f;

    public Transform playerTransform;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = playerTransform.position;
    }

    void Update()
    {
        
        timer += Time.deltaTime;

        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);

        timeText.text = $"Tiempo: {minutes:00}:{seconds:00}";


        distance = playerTransform.position.x - startPosition.x;
        distanceText.text = $"Metros: {distance:F1}m";


        coinsText.text = $"Monedas: {coins}";
    }


    public void AddCoin(int amount = 1)
    {
        coins += amount;
    }

    public void GuardarMarca()
    {
        string nivel = SceneManager.GetActiveScene().name;
        string playerName = PlayerPrefs.GetString("PlayerName", "Unknown");

        string key = nivel + "_" + playerName;

        float bestTime = PlayerPrefs.GetFloat(key + "_Time", float.MaxValue);
        if (timer < bestTime)
        {
            PlayerPrefs.SetFloat(key + "_Time", timer);
            PlayerPrefs.SetFloat(key + "_Distance", distance);
            PlayerPrefs.SetInt(key + "_Coins", coins);
            PlayerPrefs.Save();
        }

        // 👇 Llamamos a esta función para asegurarnos que el jugador esté en la lista
        AñadirJugadorAlRanking(nivel, playerName);
    }

    void AñadirJugadorAlRanking(string nivel, string nombre)
    {
        string keyLista = nivel + "_Jugadores";
        string lista = PlayerPrefs.GetString(keyLista, "");

        List<string> jugadores = new List<string>(lista.Split(','));

        if (!jugadores.Contains(nombre))
        {
            jugadores.Add(nombre);
            string nuevaLista = string.Join(",", jugadores);
            PlayerPrefs.SetString(keyLista, nuevaLista);
            PlayerPrefs.Save();
        }
    }

}
