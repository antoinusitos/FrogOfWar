using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {

	public void LaunchGame()
    {
        //Debug.Log("Menu Button : LaunchGame");
        GameManager.Instance.StartGame();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
