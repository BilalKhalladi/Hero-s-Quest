using UnityEngine;

public class RankingMenuManager : MonoBehaviour
{
    public GameObject rankingNivel1Panel;
    public GameObject rankingNivel2Panel;
    public GameObject rankingNivel3Panel;


    void Start()
    {
        MostrarRanking(1); 
    }

    public void MostrarRanking(int nivel)
    {
        rankingNivel1Panel.SetActive(nivel == 1);
        rankingNivel2Panel.SetActive(nivel == 2);
    }
}
