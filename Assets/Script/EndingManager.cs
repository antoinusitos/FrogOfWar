using UnityEngine;
using System.Collections;

public class EndingManager : MonoBehaviour {

    public GameObject UIFin;

    private static EndingManager instance;

    public static EndingManager Instance
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

    public void AfficheFin()
    {
        UIFin.SetActive(true);
    }
}
