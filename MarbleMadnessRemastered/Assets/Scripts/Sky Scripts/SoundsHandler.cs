//This script is in "Global/Scripts"
//Used to play some sounds

using UnityEngine;
using System.Collections;

public class SoundsHandler : MonoBehaviour {

    private AudioSource Audio;

    public AudioClip PierceSound;
    public AudioClip SplashSound;
    public AudioClip PlateSound;
    public AudioClip FallSound;
    public AudioClip DieSound;


    // Use this for initialization
    void Start () {
        Audio = GetComponent<AudioSource>();

    }
	
	public void play_PierceSound()
    {
        Audio.PlayOneShot(PierceSound, 0.7F);
    }

    public void play_SplashSound()
    {
        Audio.PlayOneShot(SplashSound, 0.7F);
    }

    public void play_PlateSound()
    {
        Audio.PlayOneShot(PlateSound, 0.7F);
    }

    public void play_FallSound()
    {
        Audio.PlayOneShot(FallSound, 0.7F);
    }

    public void play_DieSound()
    {
        Audio.PlayOneShot(DieSound, 0.7F);
    }
}
