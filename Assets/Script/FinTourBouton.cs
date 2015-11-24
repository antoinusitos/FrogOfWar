using UnityEngine;
using System.Collections;

public class FinTourBouton : MonoBehaviour {

	public void OnUse()
    {
        PlayerManager.Instance.EndTurn();
    }
}
