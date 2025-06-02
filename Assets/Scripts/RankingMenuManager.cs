using UnityEngine;
using TMPro;

public class RankingMenuManager : MonoBehaviour
{
    public GameObject rankingNivel1Panel;
    public GameObject rankingNivel2Panel;

    public RankingDisplay rankingDisplay;

    void Start()
    {
        MostrarRanking(1);
    }


    public void MostrarRanking(int nivel)
    {
        rankingNivel1Panel.SetActive(nivel == 1);
        rankingNivel2Panel.SetActive(nivel == 2);

        if (rankingDisplay != null)
        {
            if (nivel == 1)
                rankingDisplay.MostrarRanking("SampleScene", rankingDisplay.rankingNivel1);
            else if (nivel == 2)
                rankingDisplay.MostrarRanking("Level 2", rankingDisplay.rankingNivel2);
        }
    }
}
