﻿using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    private static InputManager instance;

    public static InputManager Instance
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

    // Update is called once per frame
    void Update ()
    {
	    if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.GetComponent<Case>())
                {
                    GameObject coord = hit.transform.gameObject;
                    //Debug.Log("x:" + coord.GetComponent<Case>().GetCoordonnees()._x + " y:" + coord.GetComponent<Case>().GetCoordonnees()._y + " index:" + coord.GetComponent<Case>().GetCoordonnees()._index + " occup:" + coord.GetComponent<Case>().GetCoordonnees()._occcupe);
                    PlayerManager.Instance.deplacement(coord);
                }
                else if (hit.transform.GetComponent<Player>())
                {
                    PlayerManager.Instance.WantToAttack(hit.transform.gameObject);
                }
                else if (hit.transform.GetComponent<Objectif>())
                {
                    hit.transform.GetComponent<Objectif>().Possess();
                    PlayerManager.Instance.PossessObjectif();
                    GameObject coord = hit.transform.gameObject;
                    PlayerManager.Instance.deplacement(coord.GetComponent<Objectif>()._case);
                }
            }
        }
	}
}