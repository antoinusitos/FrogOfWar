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

    public void InitPlateau()
    {
        _p = new GameObject();
        _p.name = "plateau";
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
}
