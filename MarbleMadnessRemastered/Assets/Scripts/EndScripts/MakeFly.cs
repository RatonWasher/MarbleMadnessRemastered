//Attach this script to a gO with a trigger collider
//Make the entering gO fly by adding Yforce on it continuously

using UnityEngine;
using System.Collections;

public class MakeFly : MonoBehaviour
{
    public float FlyForce = 2f;

    void OnTriggerStay(Collider collider)
    {
        Vector3 Eject = new Vector3(0.0f, 5f, 0.0f);
        collider.GetComponent<Rigidbody>().AddForce(Eject * FlyForce);
    }

}
