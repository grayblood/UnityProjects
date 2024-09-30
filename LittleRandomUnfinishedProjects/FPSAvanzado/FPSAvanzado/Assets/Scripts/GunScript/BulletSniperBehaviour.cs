using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSniperBehaviour : MonoBehaviour
{
    Rigidbody rb;
    public float velocityBullet = 5f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(this.gameObject, 10f);
    }
    private void Start()
    {
        rb.velocity = transform.forward * velocityBullet;
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
}
