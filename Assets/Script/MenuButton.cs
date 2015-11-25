using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {

	public void LaunchGame()
    {
        GameManager.Instance.StartGame();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
