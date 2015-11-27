using UnityEngine;
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
	    if(Input.GetMouseButtonDown(0) && GameManager.Instance._state == GameManager.GameState.inGame && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            GameObject currentPlayer = PlayerManager.Instance.GetPlayerTurn();

            if (Physics.Raycast(ray, out hit, 100))
            {
                if (hit.transform.GetComponent<Case>())
                {
                    GameObject coord = hit.transform.gameObject;
                    //Debug.Log("x:" + coord.GetComponent<Case>().GetCoordonnees()._x + " y:" + coord.GetComponent<Case>().GetCoordonnees()._y + " index:" + coord.GetComponent<Case>().GetCoordonnees()._index + " occup:" + coord.GetComponent<Case>().GetCoordonnees()._occcupe);
                    Vector3 v = new Vector3(coord.transform.position.x - currentPlayer.transform.position.x, coord.transform.position.y - currentPlayer.transform.position.y, 0);
                    Debug.DrawRay(currentPlayer.transform.position, v, Color.green, 5);
                    if (!Physics.Raycast(currentPlayer.transform.position, v, out hit, Vector3.Distance(currentPlayer.transform.position, coord.transform.position)))
                    {
                        PlayerManager.Instance.deplacement(coord);
                    }
                }
                else if (hit.transform.GetComponent<Player>())
                {
                    PlayerManager.Instance.WantToAttack(hit.transform.gameObject);
                }
                else if (hit.transform.GetComponent<Objectif>() && hit.transform.GetComponent<Objectif>().GetPossess() == false && Mathf.Round(Vector3.Distance(hit.transform.position, currentPlayer.transform.position)) <= currentPlayer.GetComponent<Player>().GetStamina())
                {
                    GameObject coord = hit.transform.gameObject;
                    hit.transform.GetComponent<MeshRenderer>().enabled = false;
                    PlayerManager.Instance.deplacement(coord.GetComponent<Objectif>()._case);
                    hit.transform.GetComponent<Objectif>().Possess(currentPlayer.GetComponent<Player>()._playerNumber);
                    PlayerManager.Instance.PossessObjectif();
                }
                else if (hit.transform.GetComponent<Loot>() && Mathf.Round(Vector3.Distance(hit.transform.position, currentPlayer.transform.position)) <= currentPlayer.GetComponent<Player>().GetStamina())
                {
                    GameObject coord = hit.transform.gameObject;
                    coord.GetComponent<Loot>().Pickup(PlayerManager.Instance.GetPlayerTurn());
                    PlayerManager.Instance.deplacement(coord.GetComponent<Loot>()._case);
                }
                else
                {
                    Debug.Log(Vector3.Distance(hit.transform.position, currentPlayer.transform.position));
                }
            }
        }
	}
}