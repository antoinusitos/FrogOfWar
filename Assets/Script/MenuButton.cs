using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {

	public void LaunchGame()
    {
        //Debug.Log("Menu Button : LaunchGame");
        SoundManager.Instance.Button();
        GameManager.Instance.Launch();
    }

    public void Tuto()
    {
        //Debug.Log("Menu Button : LaunchGame");
        SoundManager.Instance.Button();
        GameManager.Instance.Tuto();
    }

    public void Quit()
    {
        SoundManager.Instance.Button();
        Application.Quit();
    }
}
