using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {

    public GameObject P1S;
    public GameObject P2S;
    public GameObject P1M;
    public GameObject P2M;
    public GameObject P1K;
    public GameObject P2K;

    private static ScoreManager instance;

    public static ScoreManager Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void MAJScores()
    {
        GameObject P1 = PlayerManager.Instance.GetPlayer(1);
        GameObject P2 = PlayerManager.Instance.GetPlayer(2);

        P1S.GetComponent<Text>().text = P1.GetComponent<Player>().GetScore().ToString();
        P2S.GetComponent<Text>().text = P2.GetComponent<Player>().GetScore().ToString();
        P1M.GetComponent<Text>().text = P1.GetComponent<Player>().GetDeath().ToString();
        P2M.GetComponent<Text>().text = P2.GetComponent<Player>().GetDeath().ToString();
        P1K.GetComponent<Text>().text = P1.GetComponent<Player>().GetKill().ToString();
        P2K.GetComponent<Text>().text = P2.GetComponent<Player>().GetKill().ToString();
    }
}
