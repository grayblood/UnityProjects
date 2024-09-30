using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public float force = 50f;
    public int dmg = 100;
    List<GameObject> currentCollisions = new List<GameObject>();
    void Awake()
    {
        

        Destroy(gameObject, 0.9f);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Dummie")
        {
            currentCollisions.Add(other.gameObject);
            print(currentCollisions);
        }
        if (currentCollisions != null)
        {

            foreach (GameObject gObject in currentCollisions)
            {
                Enemy ene = gObject.GetComponentInParent<Enemy>();
                print(ene);

                ene.Hit(dmg);
                print("That's a lot of DAMAGE");
            }
        }
       
    }

   
}
