using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    public GameObject Menu;
    public GameObject InGame;
    public GameObject FOV;
    public GameObject Ending;


    private static UIManager instance;

    public static UIManager Instance
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

    void Start()
    {
        MenuGame();
    }

    public void MenuGame()
    {
        Menu.SetActive(true);
        FOV.SetActive(false);
        InGame.SetActive(false);
        Ending.SetActive(false);
    }

    public void LaunchGame()
    {
        InGame.SetActive(true);
        FOV.SetActive(true);
        Menu.SetActive(false);
        Ending.SetActive(false);
    }

    public void EndGame()
    {
        InGame.SetActive(false);
        FOV.SetActive(false);
        Menu.SetActive(false);
        Ending.SetActive(true);
    }
}
