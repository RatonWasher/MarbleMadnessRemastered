//Makes the entering gO die slowly and launchs splash sound

using UnityEngine;
using System.Collections;

public class Water : MonoBehaviour {

	void OnTriggerEnter()
    {
        GameObject.FindGameObjectWithTag("Sounds").
                    GetComponent<SoundsHandler>().play_SplashSound();
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.tag == "Player")
        {
            collider.GetComponent<Health>().TakeDamage(0.3f);
        }
    }

}
