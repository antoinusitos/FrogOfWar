using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerManager : MonoBehaviour {

    bool _tourJoueur1 = true; //true = joueur 1, false = joueur 2
    public GameObject _player1Prefab;
    public GameObject _player2Prefab;

    private GameObject _player1Instance;
    private GameObject _player2Instance;

    public GameObject _staminaValue;

    public GameObject _player1Score;
    public GameObject _player2Score;

    private static PlayerManager instance;

    public static PlayerManager Instance
    {
        get
        {
            return instance;
        }
    }

    // Use this for initialization
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SpawnPlayers(GameObject _plateau)
    {
        GameObject caseP1 = PlateauManager.Instance.GetCaseSpawnPlayer(1);
        GameObject caseP2 = PlateauManager.Instance.GetCaseSpawnPlayer(2);
        _player1Instance = (GameObject)Instantiate(_player1Prefab, caseP1.transform.position + new Vector3(0, 0, 1f), Quaternion.identity);
        _player1Instance.GetComponent<Player>().SetCase(caseP1);
        _player1Instance.GetComponent<Player>().SetSpawnPoint();
        _player1Instance.transform.parent = _plateau.transform;
        _player1Instance.GetComponent<Revealer>().Register();
        _player1Instance.GetComponent<Player>().Reset();
        _player2Instance = (GameObject)Instantiate(_player2Prefab, caseP2.transform.position + new Vector3(0, 0, 1f), Quaternion.identity);
        _player2Instance.GetComponent<Player>().SetCase(caseP2);
        _player2Instance.GetComponent<Player>().SetSpawnPoint();
        _player2Instance.transform.parent = _plateau.transform;
        _player2Instance.GetComponent<Revealer>().Register();
        _player2Instance.GetComponent<Player>().Reset();
    }

    public void ResetGame()
    {
        _tourJoueur1 = true;
    }

    public void RefreshScorePlayers()
    {
        _player1Score.GetComponent<Text>().text = _player1Instance.GetComponent<Player>().GetScore().ToString();
        _player2Score.GetComponent<Text>().text = _player2Instance.GetComponent<Player>().GetScore().ToString();
    }

    public void EndTurn()
    {
        //Debug.Log("PlayerManager : EndTurn");
        PlateauManager.Instance.ResetMoveCase();
        if (_tourJoueur1) // tour joueur 1
        {
            _player1Instance.GetComponent<Player>().BoostStamina();
        }
        else // tour joueur 2
        {
            _player2Instance.GetComponent<Player>().BoostStamina();
        }
        _tourJoueur1 = !_tourJoueur1;
        NewTurn();
    }

    public void NewTurn()
    {
        //Debug.Log("PlayerManager : newTurn");
        GameManager.Instance.EndTurn();
        if (_tourJoueur1) // tour joueur 1
        {
            _player1Instance.GetComponent<Player>().RefillStamina();
        }
        else // tour joueur 2
        {
            _player2Instance.GetComponent<Player>().RefillStamina();
        }
        ActualiseStaminaText();
       // PlateauManager.Instance.ShowMoveCase(currentPlayer.GetComponent<Player>().GetCase(), currentPlayer.GetComponent<Player>().GetStamina());
    }

    public void showCases()
    {
        GameObject currentPlayer = null;
        if (_tourJoueur1) // tour joueur 1
        {
            currentPlayer = _player1Instance;
        }
        else // tour joueur 2
        {
            currentPlayer = _player2Instance;
        }
        PlateauManager.Instance.ShowMoveCase(currentPlayer.GetComponent<Player>().GetCase(), currentPlayer.GetComponent<Player>().GetStamina());
    }

    public void HidePlayers()
    {
        _player1Instance.GetComponent<MeshRenderer>().enabled = false;
        for (int i = 0; i < _player1Instance.transform.childCount - 1; i++)
        {
            _player1Instance.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
        }
        _player2Instance.GetComponent<MeshRenderer>().enabled = false;
        for (int i = 0; i < _player2Instance.transform.childCount - 1; i++)
        {
            _player2Instance.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public void ShowPlayers()
    {
        _player1Instance.GetComponent<MeshRenderer>().enabled = true;
        for (int i = 0; i < _player1Instance.transform.childCount - 1; i++)
        {
            _player1Instance.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
        }
        _player2Instance.GetComponent<MeshRenderer>().enabled = true;
        for (int i = 0; i < _player2Instance.transform.childCount - 1; i++)
        {
            _player2Instance.transform.GetChild(i).GetComponent<MeshRenderer>().enabled = true;
        }
    }

    public void deplacement(GameObject theCase)
    {
        GameObject currentPlayer = null;
        if (_tourJoueur1) // tour joueur 1
        {
            currentPlayer = _player1Instance;
        }
        else // tour joueur 2
        {
            currentPlayer = _player2Instance;
        }

        Case.coordonees theCoord = theCase.GetComponent<Case>().GetCoordonnees();
        GameObject currentCase = currentPlayer.GetComponent<Player>().GetCase();
        int currentStamina = currentPlayer.GetComponent<Player>().GetStamina();
        int multiply = currentPlayer.GetComponent<Player>().HasObjectif() ? 2 : 1;
        if (!theCoord._occcupe && Vector3.Distance(currentCase.transform.position, theCase.transform.position) * multiply <= currentStamina)
        {
            Vector3 diff = currentCase.transform.position - theCase.transform.position;
            if (diff.x <= -1)// va à droite
                currentPlayer.GetComponent<Player>().SetOrientation(1, 0);
            else if (diff.x >= 1)// va à gauche
                currentPlayer.GetComponent<Player>().SetOrientation(-1, 0);
            else if (diff.y >= 1)// va en bas
                currentPlayer.GetComponent<Player>().SetOrientation(0, -1);
            else if (diff.y <= -1)// va en haut
                currentPlayer.GetComponent<Player>().SetOrientation(0, 1);

            float dist = Vector3.Distance(currentCase.transform.position, theCase.transform.position);
            //Debug.Log(dist);
            float Z = 1f;
            if (theCase.GetComponent<Objectif>())
                Z = 0f;
            currentPlayer.transform.position = new Vector3(theCoord._x, theCoord._y, Z);
            theCase.GetComponent<Case>().SetOccupe(true);
            currentPlayer.GetComponent<Player>().GetCase().GetComponent<Case>().SetOccupe(false);
            currentPlayer.GetComponent<Player>().SetCase(theCase);
            currentPlayer.GetComponent<Player>().Consume((int)Mathf.Ceil(dist) * multiply);

            PlateauManager.Instance.ResetMoveCase();
            PlateauManager.Instance.ShowMoveCase(currentPlayer.GetComponent<Player>().GetCase(), currentPlayer.GetComponent<Player>().GetStamina());
            GameManager.Instance.SetCameraPos();
            SoundManager.Instance.Move();
        }
        ActualiseStaminaText();
    }

    public void ActualiseStaminaText()
    {
        if (_tourJoueur1) // tour joueur 1
        {
            _staminaValue.GetComponent<Text>().text = _player1Instance.GetComponent<Player>().GetStamina().ToString();
        }
        else
        {
            _staminaValue.GetComponent<Text>().text = _player2Instance.GetComponent<Player>().GetStamina().ToString();
        }
    }

    public void WantToAttack(GameObject player)
    {
        // tour joueur 1
        if (_tourJoueur1 && player !=_player1Instance && _player1Instance.GetComponent<Player>().GetCoutStaminaAttaque() <= _player1Instance.GetComponent<Player>().GetStamina())
        {
            int porte = _player1Instance.GetComponent<Player>().GetPorte();
            float dist = Vector3.Distance(_player1Instance.transform.position, _player2Instance.transform.position);
            if (dist <= porte)
            {
                _player1Instance.GetComponent<Player>().Attack(_player2Instance);
            }
        }
        // tour joueur 2
        else if (!_tourJoueur1 && player != _player2Instance && _player2Instance.GetComponent<Player>().GetCoutStaminaAttaque() <= _player2Instance.GetComponent<Player>().GetStamina())
        {
            int porte = _player2Instance.GetComponent<Player>().GetPorte();
            float dist = Vector3.Distance(_player1Instance.transform.position, _player2Instance.transform.position);
            if (dist <= porte)
            {
                _player2Instance.GetComponent<Player>().Attack(_player1Instance);
            }
        }
        ActualiseStaminaText();
    }

    public void PossessObjectif()
    {
        if (_tourJoueur1) // tour joueur 1
        {
            _player1Instance.GetComponent<Player>().PossessBonus();
        }
        else
        {
            _player2Instance.GetComponent<Player>().PossessBonus();
        }
    }

    public GameObject GetPlayerTurn()
    {
        if (_tourJoueur1) // tour joueur 1
        {
            return _player1Instance;
        }
        else
        {
            return _player2Instance;
        }
    }

    public int GetPlayerTurnInt()
    {
        if (_tourJoueur1) // tour joueur 1
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }

    public void ResetPlayer(int pl)
    {
        if (pl == 1) // tour joueur 1
        {
            GameObject currentCase = _player1Instance.GetComponent<Player>().GetCase();
            currentCase.GetComponent<Case>().SetOccupe(false);
            _player1Instance.GetComponent<Player>().SetCase(PlateauManager.Instance.GetCaseSpawnPlayer(1));
        }
        else
        {
            GameObject currentCase = _player2Instance.GetComponent<Player>().GetCase();
            currentCase.GetComponent<Case>().SetOccupe(false);
            _player2Instance.GetComponent<Player>().SetCase(PlateauManager.Instance.GetCaseSpawnPlayer(2));
        }
    }

    public GameObject GetPlayer(int pl)
    {
        if (pl == 1) // tour joueur 1
        {
            return _player1Instance;
        }
        else
        {
            return _player2Instance;
        }
    }
}
