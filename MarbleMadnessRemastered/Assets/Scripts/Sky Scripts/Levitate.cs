using UnityEngine;
using System.Collections;
//using System;

public class Levitate : MonoBehaviour
{

    public float Speed = 0f;
    public float Seconds = 0f;

    private float go = 1; //Tell the gO when to go up or down
    private float maxSeconds = 0;

    void Start()
    {
        maxSeconds = Seconds;
        Speed /= 300;
    }

    void Update() {
        if (Seconds > 0)
        {
            transform.position += (new Vector3(0f, 1f, 0f) * (go * Speed));

            Seconds -= Time.deltaTime;
        }

        if (Seconds <= 0)
        {
            go = -go;
            Seconds = maxSeconds;
        }
    }

}