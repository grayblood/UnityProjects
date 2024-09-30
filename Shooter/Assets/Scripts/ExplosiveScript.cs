using System.Collections;
using UnityEngine;

public class ExplosiveScript : MonoBehaviour
{

    [HideInInspector]
    public float maxDistance = 10;

    public GameObject decalHitWall;

    public float floatInfrontOfWall;

    public GameObject explosionEffect;

    public GameObject explosiveRadio;

    public GameObject trailEffect;

    public LayerMask ignoreLayer;

    [HideInInspector]
    public Rigidbody rb;
    [SerializeField]
    private float m_Speed = 20f;   // this is the projectile's speed
    [SerializeField]
    private float m_Lifespan = 5f; // this is the projectile's lifespan (in seconds)
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * m_Speed;


        Destroy(this.gameObject, m_Lifespan);

    }

   
    void OnCollisionEnter(Collision collision)
    {
        
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        Instantiate(explosionEffect, pos, rot);
       


        if (collision.transform.tag == "LevelPart"|| collision.transform.tag == "Slope")
        {
            Instantiate(decalHitWall, pos,rot.normalized);
            Instantiate(explosiveRadio, pos,rot);
        }


        Destroy(gameObject);
        
    }

    
}
