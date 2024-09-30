using System.Collections;
using UnityEngine;

public class Enemybulletspawner1 : MonoBehaviour
{

    public GameObject projectile;
    private Transform m_target;
    private bool shoot = false;
    public float m_tamano;
    public float m_time;

    public float m_spawntime;



    // Start is called before the first frame update
    void Awake()
    {
        if (m_spawntime == 0)
        {
            m_spawntime = 1;
        }
      
        StartCoroutine(Crysomemore());
    }

    // Update is called once per frame
    // f(x) = x. + Vx * t
    //f(y) = y. + Vy * t - ((9'8 * t^2) /2)
    private IEnumerator Crysomemore()
    {
        while(true)
        {
            if(shoot)
            {
                GameObject newbullet = Instantiate(projectile);

                newbullet.transform.position = transform.position;
                newbullet.GetComponent<Projectile>().Init(m_target, m_tamano, m_time);
            }
            yield return new WaitForSeconds(m_spawntime);
        }
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
          
            m_target = collision.gameObject.transform;
            shoot = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            m_target = null;
            shoot = false;
        }
    }
}
