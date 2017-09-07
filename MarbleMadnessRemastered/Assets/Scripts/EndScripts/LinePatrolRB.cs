//Attach this script to a "DarkPlayer" ennemy with a RB
//Makes it patrol linearly

using UnityEngine;
using System.Collections;

public class LinePatrolRB : MonoBehaviour {

    public float Seconds = 5.0f;
    public float Force = 0.05f;
    public bool IsStartReverse;


    private float maxSeconds = 0f; //Store the original speed
    private float go; //Used to determine when to go forward or backward
    private Rigidbody gORB;



    void Start()
    {
        if (IsStartReverse)
            go = -1f;
        else
            go = 1f;

        maxSeconds += Seconds;
        gORB = GetComponent<Rigidbody>();
    }


    void Update()
    {
        if (Seconds > 0)
        {
            gORB.velocity = new Vector3(0.0f, 0.0f, (go * Force));
            Seconds -= Time.deltaTime;
        }

        if (Seconds <= 0)
        {
            go = -go;
            Seconds += maxSeconds;
        }
    }
}
