using UnityEngine;
using System.Collections;

public class Objectif : MonoBehaviour {

    public bool _possessed;
    public GameObject _case;

	void Start ()
    {
        _possessed = false;
    }

    public void Possess()
    {
        _possessed = true;
        SoundManager.Instance.Relic();
        //Debug.Log("possess");
    }

    public void Unpossess()
    {
        _possessed = false;
    }

    public bool GetPossess()
    {
        return _possessed;
    }

    public void setCase(GameObject currentCase)
    {
        _case = currentCase;
    }

}
