using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    public GameObject _menu;
    public GameObject _inGame;
    public GameObject _tuto;
    public GameObject _fov;
    public GameObject _ending;
    public GameObject _endingImage;
    public GameObject _splash;
    public GameObject _turnButton;

    public Sprite _fiveStamina;
    public Sprite _fourStamina;
    public Sprite _threeStamina;
    public Sprite _twoStamina;
    public Sprite _oneStamina;
    public Sprite _zeroStamina;

    public Sprite _fourLife;
    public Sprite _threeLife;
    public Sprite _twoLife;
    public Sprite _oneLife;
    public Sprite _zeroLife;

    public GameObject _epee;
    public GameObject _shield;
    public GameObject _sac;

    public GameObject _spriteObject;
    public GameObject _spriteObjectlife;

    public GameObject _canvasImage;
    public GameObject _fX;
    public GameObject _fXFin;

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
        FalseEvery();
        _menu.SetActive(true);
        _canvasImage.SetActive(true);
        _fX.SetActive(true);
    }

    public void RefreshLife()
    {
        int life = PlayerManager.Instance.GetPlayerTurn().GetComponent<Player>().GetLife() + PlayerManager.Instance.GetPlayerTurn().GetComponent<Player>().GetShield();
        if(life == 0)
        {
            _spriteObjectlife.GetComponent<Image>().sprite = _zeroLife;
        }
        else if(life == 1)
        {
            _spriteObjectlife.GetComponent<Image>().sprite = _oneLife;
        }
        else if (life == 2)
        {
            _spriteObjectlife.GetComponent<Image>().sprite = _twoLife;
        }
        else if (life == 3)
        {
            _spriteObjectlife.GetComponent<Image>().sprite = _threeLife;
        }
        else if (life == 4)
        {
            _spriteObjectlife.GetComponent<Image>().sprite = _fourLife;
        }
    }

    public void ShowBonus()
    {
        GameObject currentPlayer = PlayerManager.Instance.GetPlayerTurn();
        Player p = currentPlayer.GetComponent<Player>();
        if(p.GetHasEpee())
        {
            //_epee.SetActive(true);
            _epee.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
           // _epee.SetActive(false);
            _epee.GetComponent<Image>().color = new Color(1f, 1f, 1f, .1f);
        }
        if (p.GetHasSac())
        {
            //_sac.SetActive(true);
            _sac.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            //_sac.SetActive(false);
            _sac.GetComponent<Image>().color = new Color(1f, 1f, 1f, .1f);
        }
        if (p.GetHasShield())
        {
            //_shield.SetActive(true);
            _shield.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
        else
        {
            //_shield.SetActive(false);
            _shield.GetComponent<Image>().color = new Color(1f, 1f, 1f, .1f);
        }
    }

    public void LaunchGame()
    {
        FalseEvery();
        _inGame.SetActive(true);
        _fov.SetActive(true);
        ShowBonus();
        RefreshLife();
    }

    public void HideSplash()
    {
        _splash.SetActive(false);
    }

    public void ShowSplash()
    {
        _splash.SetActive(true);
    }

    public void Tuto()
    {
        FalseEvery();
        _tuto.SetActive(true);
    }

    public void FalseEvery()
    {
        _turnButton.SetActive(false);
        _inGame.SetActive(false);
        _fov.SetActive(false);
        _menu.SetActive(false);
        _ending.SetActive(false);
        _canvasImage.SetActive(false);
        _fX.SetActive(false);
        _fXFin.SetActive(false);
        _tuto.SetActive(false);
        _endingImage.SetActive(false);
    }

    public void Turn()
    {
        FalseEvery();
        _turnButton.SetActive(true);
    }

    public void EndGame()
    {
        FalseEvery();
        _fov.SetActive(false);
        _ending.SetActive(true);
        _fXFin.SetActive(true);
        _endingImage.SetActive(true);
        _endingImage.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, _endingImage.transform.position.z);
    }
}
