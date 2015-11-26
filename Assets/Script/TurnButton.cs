using UnityEngine;
using System.Collections;

public class TurnButton : MonoBehaviour {

	public void OnClick()
    {
        //Debug.Log("Turn Button");
        GameManager.Instance.Play();
    }
}
