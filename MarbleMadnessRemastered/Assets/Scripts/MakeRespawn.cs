//When a player passes trought it, respawns the player

using UnityEngine;
using System.Collections;

public class MakeRespawn : MonoBehaviour {

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("GlobalScripts").
                GetComponent<Respawn>().GoRespawn(collider.transform);
        }
    }

}
