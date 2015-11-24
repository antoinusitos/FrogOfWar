using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    private int _life;
    private GameObject _currentCase;
    private int _stamina;
    private int _staminaMax;
    public int _palierStaminaMax;
    public int _attaque;
    public int _porte;
    public int _coutStaminaAttaque;
    public int _coutStaminaContreAttaque;
    public int _orientationX;
    public int _orientationY;
    private bool _hasContreAttack;
    private bool _possessObjectif;
    private int _score;
    public Vector3 _spawnPoint;
    public int _scoreMax;

    public Transform FogOfWarPlane;
    public int Number = 1;

    // Use this for initialization
    void Start ()
    {
        _life = 100;
        _staminaMax = 3;
        _stamina = _staminaMax;
        _palierStaminaMax = 9;
        _attaque = 30;
        _porte = 1;
        _coutStaminaAttaque = 3;
        _coutStaminaContreAttaque = 3;
        _orientationX = 0;
        _orientationY = -1;
        _hasContreAttack = false;
        _score = 0;
        _possessObjectif = false;
        _scoreMax = 5;
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Ray rayToPlayerPos = Camera.main.ScreenPointToRay(screenPos);
        int layermask = (int)(1 << 8);
        RaycastHit hit;
        if (Physics.Raycast(rayToPlayerPos, out hit, 1000, layermask))
        {
            FogOfWarPlane.GetComponent<Renderer>().material.SetVector("_Player" + Number.ToString() + "_Pos", hit.point);
        }
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
        _stamina = _staminaMax;
        SetOrientation(0, -1);
        transform.position = _spawnPoint;
    }

    public bool TakeDamage(int damage)
    {
        //Debug.Log("take damage");
        _life -= damage;
        if(_life <= 0)
        {
            Reset();
            return true;
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
            _score += 2;
            if(_possessObjectif)
            {
                _score ++;
            }
            PlayerManager.Instance.RefreshScorePlayers();

            if(_score >= _scoreMax)
            {
                Debug.Log("Partie Fini !!");
                EndingManager.Instance.AfficheFin();
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
                playerSend.GetComponent<Player>().TakeDamage(_attaque);
                Consume(_coutStaminaContreAttaque);
            }
        }
    }

    public void SetOrientation(int X, int Y)
    {
        _orientationX = X;
        _orientationY = Y;
    }
}
