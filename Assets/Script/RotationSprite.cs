using UnityEngine;
using System.Collections;

public class RotationSprite : MonoBehaviour {
	
	void Update ()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * 10);
    }
}
