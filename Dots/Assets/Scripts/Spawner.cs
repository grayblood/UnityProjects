using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Prefab;
    [SerializeField]
    private float m_SpawnTime;
    [SerializeField]
    private float m_SpawnAmount;
    void Start()
    {
        StartCoroutine("Spawn");
    }

    public IEnumerator Spawn()
    {
        while (true)
        {
            for (int i = 0; i < m_SpawnAmount; i++)
            {
                GameObject go = Instantiate(m_Prefab);
                //go.transform.position = new Vector3(Random.Range(-25, 25), Random.Range(-25, 25));
            }
            yield return new WaitForSeconds(m_SpawnTime);
        }
    }
}
