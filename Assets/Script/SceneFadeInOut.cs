using UnityEngine;
using System.Collections;

public class SceneFadeInOut : MonoBehaviour
{
    public float fadeSpeed = 0.8f;          // Speed that the screen fades to and from black.
    public Texture2D fadeOutTexture;

    private int drawDepth = -1000;
    private float alpha = 1.0f;
    private int fadeDir = -1;

    private static SceneFadeInOut instance;

    public static SceneFadeInOut Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        // Set the texture so that it is the the size of the screen and covers it.
        //GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture);
    }

    public float BeginFade(int direction)
    {
        fadeDir = direction;
        return fadeSpeed;
    }

    public void EndFade()
    {
        BeginFade(-1);
    }
}