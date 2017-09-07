//This script is in "Global/Scripts"
//Used to fade in and out during the level

using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour {

    public Texture2D fadeTexture;
    public float fadeSpeed = 0.2f;

    private int drawDepth = -1000; //Used for order
    private float alpha = 1f; //Alpha for the texture
    private int fadeDir = -1; //-1 to fade Texture->Level, 1 to fade Level->Texture

    void OnGUI()
    {
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);

        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
    }
    
    public void StartFade(int direction)
    {
        fadeDir = direction;
    }

}