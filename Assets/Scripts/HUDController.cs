using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;
using static RankingDisplay;

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

        if (Time.timeScale == 0f) return; 

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
        Debug.Log("[DEBUG] GuardarMarca() llamado");
        Debug.Log("[DEBUG] Nombre del jugador: " + playerName);

        MarcaJugador nuevaMarca = new MarcaJugador(playerName, timer, coins, distance);
        Debug.Log($"[DEBUG] Marca nueva: Tiempo {timer}, Monedas {coins}, Distancia {distance}");

        string key = nivel + "_Top5";
        string jsonGuardado = PlayerPrefs.GetString(key, "");

        MarcaJugadorLista lista;
        if (string.IsNullOrEmpty(jsonGuardado))
        {
            lista = new MarcaJugadorLista();
        }
        else
        {
            lista = JsonUtility.FromJson<MarcaJugadorLista>(jsonGuardado);
        }

        lista.marcas.Add(nuevaMarca);
        lista.marcas.Sort((a, b) => a.tiempo.CompareTo(b.tiempo));
        if (lista.marcas.Count > 5)
            lista.marcas = lista.marcas.GetRange(0, 5);

        string nuevoJson = JsonUtility.ToJson(lista);
        Debug.Log($"[DEBUG] JSON generado: {nuevoJson}");

        PlayerPrefs.SetString(key, nuevoJson);
        PlayerPrefs.Save();
        Debug.Log("[DEBUG] Marca guardada correctamente en PlayerPrefs");
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
