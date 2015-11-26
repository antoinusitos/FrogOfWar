using UnityEngine;
using System.Collections;

public class TurnButton : MonoBehaviour {

	public void OnClick()
    {
        SoundManager.Instance.Button();
        //Debug.Log("Turn Button");
        GameManager.Instance.Play();
    }
}
