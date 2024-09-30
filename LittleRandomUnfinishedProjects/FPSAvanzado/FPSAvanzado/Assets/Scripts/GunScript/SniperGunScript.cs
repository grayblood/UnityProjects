using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperGunScript : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;

    public float cadency = 2.5f;
    
    public int max_Ammo;
    public int current_Ammo;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, transform.position, transform.rotation);
        }
    }
}
