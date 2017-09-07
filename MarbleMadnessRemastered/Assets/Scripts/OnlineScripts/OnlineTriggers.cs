using UnityEngine;
using System.Collections;

public class OnlineTriggers : MonoBehaviour {

    private Transform Respawn;

    // Use this for initialization
    void Start()
    {
        Respawn = GameObject.Find("Respawn").GetComponent<Transform>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "fallout")
        {
            transform.position = Respawn.position;
            GetComponent<OnlineHealth>().resetHealth(3);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }

        if (other.tag == "Finish")
        {
            GetComponent<OnlineControls>().stopBall();
            GetComponent<OnlineControls>().loseControls();
            GetComponent<OnlineEvents>().endLvl();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            GetComponent<OnlineHealth>().decreaseHealth(1);
        }

        if (other.gameObject.tag == "Player")
        {
            GetComponent<OnlineHealth>().decreaseHealth(1);
        }

    }
}
