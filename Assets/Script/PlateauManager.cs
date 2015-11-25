using UnityEngine;
using System.Collections;

public class PlateauManager : MonoBehaviour {

    public int _longueur = 15;
    public int _largeur = 9;
    public GameObject _case;
    public GameObject[] _plateau;
    private int _totalCases = 0;
    private GameObject _p;

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
        GameObject[] retour = new GameObject[4];
        int index = GetIndex(3, 1);
        retour[0] = _plateau[index];
        index = GetIndex(11, 1);
        retour[1] = _plateau[index];
        index = GetIndex(3, 7);
        retour[2] = _plateau[index];
        index = GetIndex(11, 7);
        retour[3] = _plateau[index];
        return retour;
    }
}
