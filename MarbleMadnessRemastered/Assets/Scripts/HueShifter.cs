//Make the attached gO color shift

using UnityEngine;
using System.Collections;

public class HueShifter : MonoBehaviour {

    public float ShiftSpeed = 0.3f;

    private Renderer gO_Renderer;

    void Start()
    {
        gO_Renderer = GetComponent<Renderer>();
    }


    void Update()
    {
        gO_Renderer.material.SetColor("_Color", HSBColor.ToColor(
            new HSBColor(Mathf.PingPong(Time.time * ShiftSpeed, 1), 1, 1)));
    }

}