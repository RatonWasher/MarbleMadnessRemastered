using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {

    private static bool activated = true;
    private static GameObject sounds;
    private Button soundButton;

    void Awake()
    {
        soundButton = GameObject.Find("soundButton").GetComponent<Button>();
        soundButton.onClick.Invoke();
        DontDestroyOnLoad(gameObject);
    }


    public void stopSounds()
    {   
        activated = !activated;
        if (activated == false)
        {
            sounds.SetActive(false);
        }
        else
        {
            sounds.SetActive(true);
        }   
    }
}
