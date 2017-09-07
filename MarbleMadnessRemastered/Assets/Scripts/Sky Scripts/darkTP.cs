using UnityEngine;
using System.Collections;

public class darkTP : MonoBehaviour {

    public GameObject tpArrival;

    void OnTriggerEnter(Collider collider)
    {
        GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<Fade>().StartFade(1);

        StartCoroutine(TPtoNextPoint(1.8f, collider));
    }

    void OnTriggerStay(Collider collider)
    {
        collider.GetComponent<Controls>().Immobilize(transform.position);
    }

    IEnumerator TPtoNextPoint(float time, Collider collider)
    {
        yield return new WaitForSeconds(time);

        collider.transform.position = tpArrival.transform.position;
        collider.GetComponent<Controls>().Release();
        GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<Fade>().StartFade(-1);
    }

}