using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    
    public GameObject bala;
    public Vector2 m_direction = Vector2.right;
    public float m_distancia;
    public float m_tamano;
    public float m_spawntime;

    // Start is called before the first frame update
    void Awake()
    {
        if (m_spawntime == 0)
        {
            m_spawntime = 1;
        }
        InvokeRepeating("Crysomemore", 0f, m_spawntime);
    }

    // Update is called once per frame
    void Crysomemore()
    {
        
        GameObject newbullet = Instantiate(bala);
       
        newbullet.transform.position = transform.position;
        newbullet.GetComponent<BulletBehaviour>().direction = m_direction;
        newbullet.GetComponent<BulletBehaviour>().distanciamaxima = m_distancia;
        newbullet.GetComponent<BulletBehaviour>().tamano = m_tamano;
    }
}
