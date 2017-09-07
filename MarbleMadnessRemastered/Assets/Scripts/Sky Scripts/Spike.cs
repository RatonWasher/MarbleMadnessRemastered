//Makes the entering gO impaled if contact when going up

using UnityEngine;
using System.Collections;

public class Spike : MonoBehaviour {

    public float timeToDown = 0.02f; //Speed when entering ground (reload)
    public float timeToUp = 0.3f; //Speed when exiting ground (strike)

    public float yFullUp = 0.0f; //Y value to reach before going down
    public float yFullDown = 0.0f; //Y value to reach before going up


    private bool goUp = false; //Tells the gO when to go up or down
    private bool isImpaled = false; //Knows if a player has been empaled

    void FixedUpdate()
    {

        if (!isImpaled) {

            if (gameObject.transform.position.y >= yFullDown && !goUp)
            {
                gameObject.transform.position -= new Vector3(0.0f, timeToDown, 0.0f);
            }
            else
            {
                goUp = true;
            }

            if (gameObject.transform.position.y <= yFullUp && goUp)
            {
                gameObject.transform.position -= new Vector3(0.0f, -timeToUp, 0.0f);
            }

            if (gameObject.transform.position.y >= yFullUp)
            {
                goUp = false;
            }
        }
        else
        {
            if (gameObject.transform.position.y <= yFullUp)
            {
                gameObject.transform.position -= new Vector3(0.0f, -timeToUp, 0.0f);
            }
        }

    }


    void OnTriggerEnter(Collider collider)
    {
        if (goUp)
            StartCoroutine(Timer(3f, collider));
    }


    IEnumerator Timer(float time, Collider collider)
    {
        yield return new WaitForSeconds(time);
        collider.GetComponent<Health>().Die();
    }


    void OnTriggerStay(Collider collider)
    {
        if (goUp){
            if (!isImpaled){
                GameObject.FindGameObjectWithTag("Sounds").
                    GetComponent<SoundsHandler>().play_PierceSound();
            }

            isImpaled = true;

            collider.GetComponent<Controls>().Immobilize(
                new Vector3(transform.position.x + 0.4f,
                            transform.position.y + 3.35f,
                            transform.position.z + 0.3f));
        }
    }


    void OnTriggerExit(Collider collider)
    {
        isImpaled = false;
    }

}
