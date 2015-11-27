using UnityEngine;
using System.Collections;

public class BonusManager : MonoBehaviour {

    public GameObject _prefabObjectif;
    public GameObject _prefabBonus;
    private GameObject _objectifInstance;
    private GameObject[] _bonus;

    public Sprite _attackSprite;
    public Sprite _defenseSprite;
    public Sprite _objectSprite;

    private static BonusManager instance;

    public static BonusManager Instance
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
        _bonus = new GameObject[8];
    }

    public void SpawnObjectif(GameObject _plateau)
    {
        GameObject TheCase = PlateauManager.Instance.GetMilieu();
        _objectifInstance = (GameObject)Instantiate(_prefabObjectif, TheCase.transform.position + new Vector3(0, 0, 1), Quaternion.identity);
        _objectifInstance.GetComponent<Objectif>()._case = TheCase;
        _objectifInstance.transform.parent = _plateau.transform;
    }

    public void SpawnLoot(GameObject _plateau)
    {
        GameObject[] TheCases = PlateauManager.Instance.GetLootPos();
        for (int i = 0; i < TheCases.Length; i++)
        {
            _bonus[i] = (GameObject)Instantiate(_prefabBonus, TheCases[i].transform.position + new Vector3(0, 0, 1), Quaternion.identity);
            _bonus[i].GetComponent<Loot>()._case = TheCases[i];
            _bonus[i].transform.parent = _plateau.transform;
        }
    }

    public void PlaceObjectif(GameObject theCase)
    {
        _objectifInstance.transform.position = new Vector3(theCase.transform.position.x, theCase.transform.position.y, -1);
    }

    public void AddTourObjectif()
    {
        if(PlayerManager.Instance.GetPlayer(1).GetComponent<Player>().HasObjectif())
        {
            _objectifInstance.GetComponent<Objectif>().AjouteTour(1);
        }
        else if(PlayerManager.Instance.GetPlayer(2).GetComponent<Player>().HasObjectif())
        {
            _objectifInstance.GetComponent<Objectif>().AjouteTour(2);
        }
    }

    public void SetPosObjectif(GameObject currentCase)
    {
        _objectifInstance.transform.position = currentCase.transform.position + new Vector3(0, 0, 1);
        _objectifInstance.GetComponent<MeshRenderer>().enabled = true;
        _objectifInstance.GetComponent<Objectif>().Unpossess();
        _objectifInstance.GetComponent<Objectif>().setCase(currentCase);
    }

    public void ShowSprite(GameObject player, int theSprite)
    {
        if (theSprite != -1)
        {
            player.transform.GetChild(4).gameObject.SetActive(true);
            if (theSprite == 0)
                player.transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>().sprite = _attackSprite;
            else if (theSprite == 1)
                player.transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>().sprite = _defenseSprite;
            else if (theSprite == 2)
                player.transform.GetChild(4).gameObject.GetComponent<SpriteRenderer>().sprite = _objectSprite;
        }
        else
        {
            player.transform.GetChild(4).gameObject.SetActive(false);
        }
    }
}
