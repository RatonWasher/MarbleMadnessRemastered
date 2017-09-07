//Make the entering collider bounce

using UnityEngine;
using System.Collections;

public class PinkTornado : MonoBehaviour {

    public float BounceForce = 6f;

    private Rigidbody ColliderRB;

    void OnTriggerEnter(Collider collider)
    {
        ColliderRB = collider.GetComponent<Rigidbody>();

        collider.GetComponent<Rigidbody>().velocity = 
            new Vector3(ColliderRB.velocity.x, BounceForce, ColliderRB.velocity.z);
    }

}
