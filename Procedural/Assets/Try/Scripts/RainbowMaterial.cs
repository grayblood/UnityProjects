using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowMaterial : MonoBehaviour
{
    public float rainbowSpeed;
    private float hue;
    private float sat;
    private float bri;

    private MeshRenderer meshRenderer;
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Color.RGBToHSV(meshRenderer.material.color,out hue,out sat, out bri);
        hue += rainbowSpeed / 10000;
        if(hue >= 1)
        {
            hue = 0;
        }
        sat = 1;
        bri = 1;
        meshRenderer.material.color = Color.HSVToRGB(hue,sat,bri);

    }
}
