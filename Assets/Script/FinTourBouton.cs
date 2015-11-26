using UnityEngine;
using System.Collections;

public class FinTourBouton : MonoBehaviour {

	public void OnUse()
    {
        SoundManager.Instance.Button();
        PlayerManager.Instance.EndTurn();
    }

    public void Abandonner()
    {
        SoundManager.Instance.Button();
        GameManager.Instance.FinGame();
    }

    public void Menu()
    {
        SoundManager.Instance.Button();
        GameManager.Instance.MenuGame();
    }
}
