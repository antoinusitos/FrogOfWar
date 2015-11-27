using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

    public bool hasAttack = false;
    public bool hasBeenViewed = false;
    public bool haveInfo = false;

    private static EventManager instance;

    public static EventManager Instance
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

    public void HasAttacked()
    {
        haveInfo = true;
        hasAttack = true;
    }

    public void HasBeenViewed()
    {
        haveInfo = true;
        hasBeenViewed = true;
    }

    public void Reset()
    {
        hasBeenViewed = false;
        hasAttack = false;
        haveInfo = false;
    }
}
