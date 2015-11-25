using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public int _playerNumber;
    private int _life;
    private GameObject _currentCase;
    private int _stamina;
    private int _staminaMax;
    public int _palierStaminaMax;
    public int _attaque;
    public int _porte;
    public int _shield;
    public int _kills;
    public int _death;
    public int _coutStaminaAttaque;
    public int _coutStaminaContreAttaque;
    public int _orientationX;
    public int _orientationY;
    private bool _hasContreAttack;
    private bool _possessObjectif;
    private int _score;
    private int _pieges;
    public Vector3 _spawnPoint;
    public int _scoreMax;

    public Transform FogOfWarPlane;
    public int Number = 1;

    public GameObject ConeD;
    public GameObject ConeG;
    public GameObject ConeH;
    public GameObject ConeB;

    // Use this for initialization
    void Start ()
    {
        _life = 100;
        _staminaMax = 2;
        _stamina = _staminaMax;
        _palierStaminaMax = 5;
        _attaque = 20;
        _porte = 1;
        _coutStaminaAttaque = 3;
        _coutStaminaContreAttaque = 3;
        _orientationX = 0;
        _orientationY = -1;
        _hasContreAttack = false;
        _score = 0;
        _possessObjectif = false;
        _scoreMax = 5;
        _shield = 0;
        _kills = 0;
        _death = 0;
        PlayerManager.Instance.ActualiseStaminaText();
        PlayerManager.Instance.NewTurn();
    }

    public GameObject GetCase()
    {
        return _currentCase;
    }

    public void SetCase(GameObject newCase)
    {
        _currentCase = newCase;
    }

    public int GetStamina()
    {
        return _stamina;
    }

    public int GetPorte()
    {
        return _porte;
    }

    public int GetScore()
    {
        return _score;
    }

    public int GetCoutStaminaAttaque()
    {
        return _coutStaminaAttaque;
    }

    public void SetStamina(int newStam)
    {
        _stamina = newStam;
    }

    public void RefillStamina()
    {
        _staminaMax = Mathf.Min(_staminaMax, _palierStaminaMax);
        _stamina = _staminaMax;
        _hasContreAttack = false;
    }

    public void BoostStamina()
    {
        _staminaMax++;
    }

    public void Consume(int deplacement)
    {
        _stamina -= deplacement;
    }

    public void Reset()
    {
        _life = 100;
        //_staminaMax = 3;
        _stamina = _staminaMax;
        SetOrientation(0, -1);
        transform.position = _spawnPoint;
        PlayerManager.Instance.ResetPlayer(_playerNumber);
    }

    public bool TakeDamage(int damage)
    {
        //Debug.Log("take damage");
        int degats = damage;
        int prevShield = _shield;
        _shield -= degats;
        degats -= prevShield;
        if (degats > 0)
        {
            _life -= degats;
            if (_life <= 0)
            {
                _death++;
                Reset();
                return true;
            }
        }
        return false;
    }

    public void SetSpawnPoint()
    {
        _spawnPoint = transform.position;
    }

    public void PossessBonus()
    {
        _possessObjectif = true;
    }

    public void UnpossessBonus()
    {
        _possessObjectif = false;
    }

    public void Attack(GameObject playerSend)
    {
        Consume(_coutStaminaAttaque);
        bool dead = playerSend.GetComponent<Player>().TakeDamage(_attaque);
        playerSend.GetComponent<Player>().ContreAttack(gameObject);
        if(dead)
        {
            _kills++;
            _score += 2;
            if(_possessObjectif)
            {
                _score ++;
            }
            PlayerManager.Instance.RefreshScorePlayers();

            if(_score >= _scoreMax)
            {
                Debug.Log("Partie Fini !!");
                GameManager.Instance.FinGame();
            }
        }
    }

    public void ContreAttack(GameObject playerSend)
    {
        if(!_hasContreAttack && _stamina >= _coutStaminaContreAttaque)
        {
            Vector3 diff = transform.position - playerSend.transform.position;
           // Debug.Log(diff.x);
            bool canContreAttack = false;
            if (diff.x >= 1 && _orientationX == -1)// va à gauche
                canContreAttack = true;
            else if (diff.x <= -1 && _orientationX == 1)// va à droite
                canContreAttack = true;
            else if (diff.y <= -1 && _orientationY == 1)// va en haut
                canContreAttack = true;
            else if (diff.y >= 1 && _orientationY == -1)// va en bas
                canContreAttack = true;

            if (canContreAttack)
            {
                _hasContreAttack = true;
                Consume(_coutStaminaContreAttaque);
                bool dead = playerSend.GetComponent<Player>().TakeDamage(_attaque);
                playerSend.GetComponent<Player>().ContreAttack(gameObject);
                if (dead)
                {
                    _kills++;
                    _score += 2;
                    if (_possessObjectif)
                    {
                        _score++;
                    }
                    PlayerManager.Instance.RefreshScorePlayers();

                    if (_score >= _scoreMax)
                    {
                        Debug.Log("Partie Fini !!");
                        GameManager.Instance.FinGame();
                    }
                }
            }
        }
    }

    public void SetOrientation(int X, int Y)
    {
        _orientationX = X;
        _orientationY = Y;
        HideCone();
        if(_orientationX == 1 && _orientationY == 0)
        {
            ConeD.SetActive(true);
        }
        else if (_orientationX == -1 && _orientationY == 0)
        {
            ConeG.SetActive(true);
        }
        else if (_orientationX == 0 && _orientationY == 1)
        {
            ConeH.SetActive(true);
        }
        else if (_orientationX == 0 && _orientationY == -1)
        {
            ConeB.SetActive(true);
        }
    }

    public void HideCone()
    {
        ConeD.SetActive(false);
        ConeG.SetActive(false);
        ConeH.SetActive(false);
        ConeB.SetActive(false);
    }

    public void AjouterAttaque(int value)
    {
        _attaque += value;
    }

    public void AjouterShield(int value)
    {
        _shield += value;
    }

    public void AjouterPiege(int value)
    {
        _pieges += value;
    }
}
