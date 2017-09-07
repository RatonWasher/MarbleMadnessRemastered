//Attach this script to a gO
//Actualizes the respawn point of the collider with the attached gO' pos

using UnityEngine;
using System.Collections;

public class SetRespawn : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        collider.GetComponent<Controls>().SetRespawnPos(
                            new Vector3(transform.position.x,
                                        transform.position.y + 2f,
                                        transform.position.z));
    }

}
