using UnityEngine;
using System.Collections;

public class Eject : MonoBehaviour {

    public float EjectForce = 2f;

    void OnTriggerStay(Collider collider)
    {
        Vector3 TornadoEject = new Vector3(collider.transform.position.x -
                                    transform.position.x, 5f, collider.transform.position.z -
                                    transform.position.z);

        collider.GetComponent<Rigidbody>().AddForce(TornadoEject * EjectForce);
    }

}
