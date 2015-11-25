using UnityEngine;
using System.Collections;

public class FinTourBouton : MonoBehaviour {

	public void OnUse()
    {
        PlayerManager.Instance.EndTurn();
    }

    public void Abandonner()
    {
        GameManager.Instance.FinGame();
    }

    public void Menu()
    {
        GameManager.Instance.MenuGame();
    }
}
