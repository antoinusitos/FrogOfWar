using UnityEngine;
using System.Collections;

public class RetourBouton : MonoBehaviour {

	public void OnUse()
    {
        SoundManager.Instance.Button();
        GameManager.Instance.MenuGame();
    }
}
