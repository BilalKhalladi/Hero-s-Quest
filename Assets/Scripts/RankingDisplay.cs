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

    void MostrarRanking(string nivel, TMP_Text textoUI)
    {
        string key = nivel + "_Top5";
        string json = PlayerPrefs.GetString(key, "");

        if (string.IsNullOrEmpty(json))
        {
            textoUI.text = "No hay datos aún.";
            return;
        }

        MarcaJugadorLista lista = JsonUtility.FromJson<MarcaJugadorLista>(json);

        if (lista == null || lista.marcas.Count == 0)
        {
            textoUI.text = "No hay datos aún.";
            return;
        }

        string textoFinal = "";
        int posicion = 1;

        foreach (var entry in lista.marcas)
        {
            int min = Mathf.FloorToInt(entry.tiempo / 60f);
            int seg = Mathf.FloorToInt(entry.tiempo % 60f);
            textoFinal += $"{posicion}. {entry.nombre} - {min:00}:{seg:00} - {entry.monedas} monedas - {entry.distancia:F1} m\n";
            posicion++;
        }

        textoUI.text = textoFinal;
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


    List<string> PlayerPrefsKeys()
    {
        return new List<string>();
    }
}

