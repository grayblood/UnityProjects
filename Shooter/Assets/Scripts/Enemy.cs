using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject m_destinationMarkerPrefab;
    private NavMeshAgent m_agent;

    [SerializeField]
    private int hpMax;
    public int hp;

    [SerializeField]
    private bool PJvivo;

    [SerializeField]
    private bool Enemyvivo;


    void Awake()
    {
        m_agent = GetComponent<NavMeshAgent>();
        hp = hpMax;
        PJvivo = true;
        Enemyvivo = true;
    }

    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Player").Length <= 0)
        {
            PJvivo = false;
        }
        if (!m_agent.pathPending && PJvivo && Enemyvivo)
        {
            SetNewDestination();
        }
        if (hp <= 0)
        {
            Enemyvivo = false;
            StartCoroutine(Destroyer());
            
        }

    }

    IEnumerator Destroyer()
    {
        Animator anim = this.GetComponent<Animator>();
        anim.enabled = false;

        Vector3 newDestination = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        m_agent.SetDestination(newDestination);
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    private void SetNewDestination()
    {

            Vector3 newDestination = new Vector3(m_destinationMarkerPrefab.transform.position.x, m_destinationMarkerPrefab.transform.position.y, m_destinationMarkerPrefab.transform.position.z);
            m_agent.SetDestination(newDestination);
    }

    public void Hit(int DMG)
    {
        hp -= DMG;
        print("Me ha hecho daño: " + DMG);
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.transform.tag == "Player")
        {
            PlayerHP php = col.transform.GetComponent<PlayerHP>();
            php.PJHit(125);
        }
    }

}
