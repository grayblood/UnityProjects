using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{

    
    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Axe" || collision.tag == "Bullet") {
            Debug.Log("L'escut ha borrat : "+collision.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
