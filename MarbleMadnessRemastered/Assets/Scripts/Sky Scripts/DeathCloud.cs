//Attach this script to a "Cloud" enemy type gO
//Makes the gO catch the player and makes him die in the air

using UnityEngine;
using System.Collections;

public class DeathCloud : MonoBehaviour
{

    public bool isMoving;

    private Vector3 originPos; //Store the original position to return after killing player
    private float Speed = 0.1f;
    private bool canFly = false; //Know if the cloud can start fly

    void OnTriggerEnter(Collider collider)
    {
        if (!collider.GetComponent<Health>().getIsCatched())
        {
            if (isMoving)
                transform.parent.GetComponent<CircularPatrol>().enabled = false;

            originPos = gameObject.transform.position;

            StartCoroutine(Fly(3f, collider));

            collider.GetComponent<Health>().setIsCatched(true);
            canFly = true;
        }
    }


    void OnTriggerStay(Collider collider)
    {
        if (canFly)
        {
            Speed += 0.002f; //Speed is progressive

            gameObject.transform.position += (new Vector3(0f, Speed, 0f) * Speed);

            collider.GetComponent<Controls>().Immobilize(
                    new Vector3(transform.position.x + (0.3f),
                                transform.position.y + 0f,
                                transform.position.z + 0f));
        }
    }


    IEnumerator Fly(float time, Collider collider)
    {
        yield return new WaitForSeconds(time);
        collider.GetComponent<Health>().Die();
    }


    void OnTriggerExit(Collider collider) //When player is dead, return on initial position
    {
        if (canFly)
        {
            collider.GetComponent<Health>().setIsCatched(false);

            gameObject.transform.position = originPos;

            if (isMoving)
                transform.parent.GetComponent<CircularPatrol>().enabled = true;

            Speed = 0.1f;
            canFly = false;
        }
    }
}
