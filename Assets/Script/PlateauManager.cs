using UnityEngine;
using System.Collections;

public class PlateauManager : MonoBehaviour {

    public int _longueur = 15;
    public int _largeur = 9;
    public GameObject _case;
    public GameObject _mur;
    public GameObject[] _plateau;
    private int _totalCases = 0;
    private GameObject _p;
    GameObject[] CaseToMove;
    GameObject[] mursLD;

    public Material _walkable;
    public Material _defaultMat;

    private static PlateauManager instance;

    public static PlateauManager Instance
    {
        get
        {
            return instance;
        }
    }

    // Use this for initialization
    void Awake ()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void InitPlateau(GameObject _p)
    {
        _totalCases = _longueur * _largeur;
        _plateau = new GameObject[_totalCases];
        for (int i = 0; i < _totalCases; ++i)
        {
            int theX = i % _longueur;
            int theY = (i - theX) / _longueur;
            if (_case == null)
                Debug.Log("lol");
            GameObject theCase = (GameObject)Instantiate(_case, new Vector3((float)theX, (float)theY, 0), Quaternion.identity);
            theCase.GetComponent<Case>().Init(theX, theY, i);
            _plateau[i] = theCase;
            theCase.transform.parent = _p.transform;
        }
        PlaceLD(_p);
    }

    public GameObject GetCase(int X, int Y)
    {
        int index = (Y * _longueur) + X;
        if (index < _totalCases)
        {
            return _plateau[index];
        }
        else
        {
            return null;
        }
    }

    public int GetIndex(int X, int Y)
    {
        return (Y * _longueur) + X;
    }

    public GameObject GetCaseSpawnPlayer(int nb)
    {
        if(nb == 1)
        {
            return GetCase(0, _largeur / 2);
        }
        else if (nb == 2)
        {
            return GetCase(_longueur - 1, _largeur / 2);
        }
        else
        {
            Debug.Log("fail to get a case for the player " + nb);
            return null;
        }
    }

    public GameObject GetMilieu()
    {
        int index = ((_largeur/2) * _longueur) + _longueur/2;
        return _plateau[index];
    }

    public GameObject[] GetLootPos()
    {
        GameObject[] retour = new GameObject[8];
        int index = GetIndex(3, 0);
        retour[0] = _plateau[index];
        index = GetIndex(8, 0);
        retour[1] = _plateau[index];
        index = GetIndex(12, 0);
        retour[2] = _plateau[index];
        index = GetIndex(5, 4);
        retour[3] = _plateau[index];
        index = GetIndex(9, 4);
        retour[4] = _plateau[index];
        index = GetIndex(2, 8);
        retour[5] = _plateau[index];
        index = GetIndex(6, 8);
        retour[6] = _plateau[index];
        index = GetIndex(11, 8);
        retour[7] = _plateau[index];
        return retour;
    }

    public void ShowMoveCase(GameObject theCase, int stamina)
    {
        CaseToMove = new GameObject[(int)Mathf.Pow(8, stamina)];
        int index = 0;
        GameObject currentPlayer = PlayerManager.Instance.GetPlayerTurn();
        int multiply = currentPlayer.GetComponent<Player>().HasObjectif() ? 2 : 1;
        for (int i = 0; i < _plateau.Length; i++)
        {
            if (theCase != _plateau[i] && Mathf.Ceil(Vector3.Distance(theCase.transform.position, _plateau[i].transform.position)) * multiply <= stamina)
            {
                CaseToMove[index] = _plateau[i];
                index++;
            }
        }

        for (int i = 0; i < CaseToMove.Length; i++)
        {
            if(CaseToMove[i] != null)
            {
                CaseToMove[i].GetComponent<Case>().Walkable();
            }
        }
    }

    public void ResetMoveCase()
    {
        for (int i = 0; i < CaseToMove.Length; i++)
        {
            if (CaseToMove[i] != null)
            {
                CaseToMove[i].GetComponent<Case>().ResetMaterial();
            }
        }
    }

    public void PlaceLD(GameObject _p)
    {
        mursLD = new GameObject[24];
        GameObject[] murs = new GameObject[24];
        int index = 0;
        murs[index] = GetCase(0, 0);
        index++;
        murs[index] = GetCase(10, 0);
        index++;
        murs[index] = GetCase(2, 1);
        index++;
        murs[index] = GetCase(3, 1);
        index++;
        murs[index] = GetCase(4, 1);
        index++;
        murs[index] = GetCase(8, 1);
        index++;
        murs[index] = GetCase(13, 1);
        index++;
        murs[index] = GetCase(13, 2);
        index++;
        murs[index] = GetCase(6, 2);
        index++;
        murs[index] = GetCase(4, 3);
        index++;
        murs[index] = GetCase(4, 4);
        index++;
        murs[index] = GetCase(4, 5);
        index++;
        murs[index] = GetCase(10, 3);
        index++;
        murs[index] = GetCase(10, 4);
        index++;
        murs[index] = GetCase(10, 5);
        index++;
        murs[index] = GetCase(1, 6);
        index++;
        murs[index] = GetCase(1, 7);
        index++;
        murs[index] = GetCase(8, 6);
        index++;
        murs[index] = GetCase(6, 7);
        index++;
        murs[index] = GetCase(10, 7);
        index++;
        murs[index] = GetCase(11, 7);
        index++;
        murs[index] = GetCase(12, 7);
        index++;
        murs[index] = GetCase(4, 8);
        index++;
        murs[index] = GetCase(14, 8);
        index++;
        for (int i = 0; i < murs.Length; ++i)
        {
            if (murs[i] != null)
            {
                mursLD[i] = (GameObject)Instantiate(_mur, murs[i].transform.position + new Vector3(0, 0, 1), Quaternion.identity);
                mursLD[i].transform.parent = _p.transform;
            }
            else
            {
                Debug.Log("null");
            }
        }
    }
}
