using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    private static GameManager instance;

    public static GameManager Instance
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

    // Use this for initialization
    void Start ()
    {
        PlateauManager.Instance.InitPlateau();
        PlayerManager.Instance.SpawnPlayers();
	}
}
