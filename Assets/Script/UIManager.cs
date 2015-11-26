using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    public GameObject _menu;
    public GameObject _inGame;
    public GameObject _fov;
    public GameObject _ending;
    public GameObject _turnButton;


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
        _turnButton.SetActive(false);
        _menu.SetActive(true);
        _fov.SetActive(false);
        _inGame.SetActive(false);
        _ending.SetActive(false);
    }

    public void LaunchGame()
    {
        _turnButton.SetActive(false);
        _inGame.SetActive(true);
        _fov.SetActive(true);
        _menu.SetActive(false);
        _ending.SetActive(false);
    }

    public void Turn()
    {
        _turnButton.SetActive(true);
        _inGame.SetActive(false);
        _fov.SetActive(false);
        _menu.SetActive(false);
        _ending.SetActive(false);
    }

    public void EndGame()
    {
        _turnButton.SetActive(false);
        _inGame.SetActive(false);
        _fov.SetActive(true);
        _menu.SetActive(false);
        _ending.SetActive(true);
    }
}
