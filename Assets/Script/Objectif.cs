using UnityEngine;
using System.Collections;

public class Objectif : MonoBehaviour {

    public bool _possessed;
    public GameObject _case;
    public int nbTour;
    public int playerPosses;

	void Start ()
    {
        _possessed = false;
        nbTour = 0;
        playerPosses = 0;
    }

    public void Possess(int p)
    {
        _possessed = true;
        SoundManager.Instance.Relic();
        playerPosses = p;
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

    public void AjouteTour(int player)
    {
        if(playerPosses == player)
        {
            nbTour++;
            if(nbTour == 4)
            {
                PlayerManager.Instance.GetPlayer(playerPosses).GetComponent<Player>().AddScore();
                Destroy(gameObject);
            }
        }
        else
        {
            playerPosses = player;
            nbTour = 0;
        }
    }

}
