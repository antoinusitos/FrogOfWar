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
    }

    public void BoostStamina()
    {
        _staminaMax++;
    }

    public void Consume(int deplacement)
    {
        _stamina -= deplacement;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("take damage");
        _life -= damage;
        if(_life <= 0)
        {
            Debug.Log("mort");
        }
    }

    public void Attack(GameObject playerSend)
    {
        Consume(_coutStaminaAttaque);
        playerSend.GetComponent<Player>().TakeDamage(_attaque);
        playerSend.GetComponent<Player>().ContreAttack(gameObject);
    }

    public void ContreAttack(GameObject playerSend)
    {
        if(!_hasContreAttack && _stamina >= _coutStaminaContreAttaque)
        {
            _hasContreAttack = true;
            playerSend.GetComponent<Player>().TakeDamage(_attaque);
            Consume(_coutStaminaContreAttaque);
        }
    }
}
