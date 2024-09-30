using UnityEngine;

public class AxeSpawner : MonoBehaviour
{
    
    public GameObject bala;
    public Vector2 m_direction = Vector2.right;
    public float m_Speed;
    public float m_spawntime;
    public float m_tamano;
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
        
        GameObject newbullet = Instantiate(bala, transform);
        newbullet.transform.position = transform.position;
        newbullet.GetComponent<AxeBehaviour>().direction = m_direction;
        newbullet.GetComponent<AxeBehaviour>().Speed = m_Speed;
        newbullet.GetComponent<AxeBehaviour>().tamano = m_tamano;

    }
}
