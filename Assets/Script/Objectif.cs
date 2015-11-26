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
        //Debug.Log("possess");
    }

    public void Unpossess()
    {
        _possessed = false;
    }

    public void setCase(GameObject currentCase)
    {
        _case = currentCase;
    }

}
