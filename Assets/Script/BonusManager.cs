using UnityEngine;
using System.Collections;

public class BonusManager : MonoBehaviour {

    public GameObject _prefabObjectif;
    private GameObject _objectifInstance;

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
    }

    public void SpawnObjectif()
    {
        GameObject TheCase = PlateauManager.Instance.GetMilieu();
        _objectifInstance = (GameObject)Instantiate(_prefabObjectif, TheCase.transform.position - new Vector3(0, 0, 1), Quaternion.identity);
        _objectifInstance.GetComponent<Objectif>()._case = TheCase;
    }
}
