using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Enemyaxespawner : MonoBehaviour
{
    public GameObject bala;

    // private List<GameObject> targets = new List<GameObject>();

    public GameObject target;
    public float m_Speed;
    public float m_spawntime;
    public float m_tamano;
    private List<GameObject> targets = new List<GameObject>();
    //change
    public Vector2 m_direction = Vector2.right;
    // Start is called before the first frame update

    void Awake()
    {
        if (m_spawntime == 0)
        {
            m_spawntime = 1;
        }
        //spawn a lot of bullets
        //InvokeRepeating("Crysomemore", 0f, m_spawntime);
    }
   
    void Crysomemore()
    {

        GameObject newbullet = Instantiate(bala, transform);
        newbullet.transform.position = transform.position;
        newbullet.GetComponent<AxeBehaviour>().direction = m_direction;
        newbullet.GetComponent<AxeBehaviour>().Speed = m_Speed;
        newbullet.GetComponent<AxeBehaviour>().tamano = m_tamano;

    }

}
