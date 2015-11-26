using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    public GameObject _menu;
    public GameObject _inGame;
    public GameObject _fov;
    public GameObject _ending;
    public GameObject _turnButton;

    public Sprite _fiveStamina;
    public Sprite _fourStamina;
    public Sprite _threeStamina;
    public Sprite _twoStamina;
    public Sprite _oneStamina;
    public Sprite _zeroStamina;

    public GameObject _spriteObject;

    public GameObject _canvasImage;
    public GameObject _fX;

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

    public void SetSpriteStamina(int nb)
    {
        if (nb == 0)
            _spriteObject.GetComponent<Image>().sprite = _zeroStamina;
        else if (nb == 1)
            _spriteObject.GetComponent<Image>().sprite = _oneStamina;
        else if (nb == 2)
            _spriteObject.GetComponent<Image>().sprite = _twoStamina;
        else if (nb == 3)
            _spriteObject.GetComponent<Image>().sprite = _threeStamina;
        else if (nb == 4)
            _spriteObject.GetComponent<Image>().sprite = _fourStamina;
        else if (nb == 5)
            _spriteObject.GetComponent<Image>().sprite = _fiveStamina;
    }

    public void MenuGame()
    {
        _turnButton.SetActive(false);
        _menu.SetActive(true);
        _fov.SetActive(false);
        _inGame.SetActive(false);
        _canvasImage.SetActive(true);
        _ending.SetActive(false);
        _fX.SetActive(true);
    }

    public void LaunchGame()
    {
        _turnButton.SetActive(false);
        _inGame.SetActive(true);
        _fov.SetActive(true);
        _menu.SetActive(false);
        _ending.SetActive(false);
        _canvasImage.SetActive(false);
        _fX.SetActive(false);
    }

    public void Turn()
    {
        _turnButton.SetActive(true);
        _inGame.SetActive(false);
        _fov.SetActive(false);
        _menu.SetActive(false);
        _ending.SetActive(false);
        _canvasImage.SetActive(false);
        _fX.SetActive(false);
    }

    public void EndGame()
    {
        _turnButton.SetActive(false);
        _inGame.SetActive(false);
        _fov.SetActive(true);
        _menu.SetActive(false);
        _ending.SetActive(true);
        _canvasImage.SetActive(false);
        _fX.SetActive(false);
    }
}
