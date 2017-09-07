//Makes the entering collider move a little in a specified direction

using UnityEngine;
using System.Collections;

public class Wind : MonoBehaviour {

    public float xForce = 0.0f;
    public float yForce = 0.0f;
    public float zForce = 0.0f;

    void OnTriggerStay(Collider collider){
        collider.GetComponent<Rigidbody>().AddForce(new Vector3(xForce, yForce, zForce));

    }
}
