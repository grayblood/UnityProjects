using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : Projectile 
{


    public override void Init(Transform target, float tiempo)
    {
        float m_directionx = target.position.x - transform.position.x;
        float m_directiony = target.position.y - transform.position.y + ((4 * 2) / 2);
        float m_directionz = target.position.z - transform.position.z ;

       // print(target);
        Vector3 m_direction = new Vector3(m_directionx, m_directiony, m_directionz);
      
        GetComponent<Rigidbody>().AddForce(m_direction*25, ForceMode.Impulse);
       // print(m_direction);
        //print(m_direction * 1000);
        Destroy(gameObject, tiempo);
    

    
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "ResPos")
        {
            Destroy(this);

        }

    }
}
