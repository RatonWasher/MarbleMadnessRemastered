//Attach this script to a "PivotPoint" gO
//Rotates the gO -> its children move in circle

using UnityEngine;
using System.Collections;

public class CircularPatrol : MonoBehaviour {

    [Range(-250, 250)]public float Speed = -65.0f;


    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * Speed);
    }
}