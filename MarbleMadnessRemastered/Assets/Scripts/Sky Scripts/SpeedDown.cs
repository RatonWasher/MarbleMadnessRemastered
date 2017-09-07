//Attach this script to an invisible gO with a trigger collider
//Slow down the gO that passes trought it

using UnityEngine;
using System.Collections;

public class SpeedDown : MonoBehaviour {

	void OnTriggerEnter(Collider collider)
    {
        collider.GetComponent<Rigidbody>().velocity *= 0.35f;
    }
}
