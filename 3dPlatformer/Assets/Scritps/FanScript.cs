using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanScript : MonoBehaviour
{
    public Transform ColliderTransform;
    void Start()
    {
        Collider collider = ColliderTransform.GetChild(0).GetComponent<Collider>();
        // do whatever you want with the collider
        collider.enabled = true;



    }
}
