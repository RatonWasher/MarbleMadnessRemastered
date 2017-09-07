//Attach this script to a "Plate" type gO
//Destroys a gO when a gO triggers the plate

using UnityEngine;
using System.Collections;

public class ActivatePlate : MonoBehaviour {

    public GameObject doorToDestroy;
    public Material activatedMat; //New material after activation

    void OnTriggerEnter(Collider collider)
    {
        if (doorToDestroy != null)
        {
            Destroy(doorToDestroy);

            GameObject.FindGameObjectWithTag("Sounds").
                GetComponent<SoundsHandler>().play_PlateSound();

            GetComponent<Renderer>().material = activatedMat;
        }
    }

}
