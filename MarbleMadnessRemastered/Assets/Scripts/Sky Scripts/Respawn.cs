//This script is in "Global/Scripts"
//Makes the given player respawn

using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {

    public void GoRespawn(Transform player)
    {
        Rigidbody ColliderRB = player.GetComponent<Rigidbody>();

        ColliderRB.velocity = Vector3.zero;
        ColliderRB.angularVelocity = Vector3.zero;

        ColliderRB.transform.position = player.GetComponent<Controls>().GetRespawnPos();
        player.GetComponent<Controls>().Release();
    }

}
