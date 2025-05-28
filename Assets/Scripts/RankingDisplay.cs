using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class RankingDisplay : MonoBehaviour
{
    public TMP_Text rankingNivel1;
    public TMP_Text rankingNivel2;

    void Start()
    {
        MostrarRanking("Level 1", rankingNivel1);
        MostrarRanking("Level 2", rankingNivel2);
    }

    void MostrarRanking(string nivel, TMP_Text rankingNivel2)
    {
        string keyLista = nivel + "_Jugadores";
        string lista = PlayerPrefs.GetString(keyLista, "");
        string[] jugadores = lista.Split(',');

        List<MarcaJugador> ranking = new List<MarcaJugador>();

        foreach (string jugador in jugadores)
        {
            if (string.IsNullOrWhiteSpace(jugador)) continue;

            float tiempo = PlayerPrefs.GetFloat(nivel + "_" + jugador + "_Time", float.MaxValue);
            int monedas = PlayerPrefs.GetInt(nivel + "_" + jugador + "_Coins", 0);
            float distancia = PlayerPrefs.GetFloat(nivel + "_" + jugador + "_Distance", 0f);

            ranking.Add(new MarcaJugador(jugador, tiempo, monedas, distancia));
        }

        ranking.Sort((a, b) => a.tiempo.CompareTo(b.tiempo));

        foreach (MarcaJugador entry in ranking)
        {
            Debug.Log($"Jugador: {entry.nombre} - Tiempo: {entry.tiempo} - Monedas: {entry.monedas} - Distancia: {entry.distancia}");
        }
    }

    class MarcaJugador
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


    List<string> PlayerPrefsKeys()
    {
        return new List<string>(); // de momento vacío, podemos resolverlo en el siguiente paso si te interesa
    }
}

