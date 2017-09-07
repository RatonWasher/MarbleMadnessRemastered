//Makes the entering gO teleports to another location

using UnityEngine;
using System.Collections;

public class Tp : MonoBehaviour {

    public GameObject tpArrival;

    void OnTriggerEnter(Collider collider)
    {
        GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<Fade>().StartFade(1);

        collider.GetComponent<Controls>().Freeze();

        StartCoroutine(TPtoNextPoint(1.8f, collider));
    }


    IEnumerator TPtoNextPoint(float time, Collider collider)
    {
        yield return new WaitForSeconds(time);

        collider.transform.position = new Vector3(
                                        tpArrival.transform.position.x,
                                        tpArrival.transform.position.y + 1,
                                        tpArrival.transform.position.z);
        collider.GetComponent<Controls>().Release();
        GameObject.FindGameObjectWithTag("GlobalScripts").GetComponent<Fade>().StartFade(-1);
    }

}