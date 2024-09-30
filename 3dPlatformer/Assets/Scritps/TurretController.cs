using System.Collections;
using UnityEngine;
public class TurretController : MonoBehaviour
{
    public GameObject bala;
    [SerializeField] GameObject direccion; //para el projectil
    [SerializeField]private  float m_time;
    public float m_spawntime;
   
    // Start is called before the first frame update

    void Awake()
    {


       
        if (m_spawntime == 0)
        {
            m_spawntime = 1;
        }
        InvokeRepeating("Crysomemore", 0f, Random.Range(4f, 10f));
    }

    // Update is called once per frame
    void Crysomemore()
    {

        GameObject newbullet = Instantiate(bala, transform);
        newbullet.transform.position = transform.position;
        newbullet.GetComponent<Projectile>().Init(direccion.transform,m_time);
        
        

    }

}
