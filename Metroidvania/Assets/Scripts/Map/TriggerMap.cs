using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMap : MonoBehaviour
{
    [SerializeField]
    private GameObject[] m_mapsOn;
    [SerializeField]
    private GameObject[] m_triggersOn;
    [SerializeField]
    private GameObject[] m_mapsOff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            for(int i = 0; i < m_mapsOn.Length; i++)
            {
                m_mapsOn[i].SetActive(true);
            }
            for (int i = 0; i < m_triggersOn.Length; i++)
            {
                m_triggersOn[i].SetActive(true);
            }
            for (int i = 0; i < m_mapsOff.Length; i++)
            {
                m_mapsOff[i].SetActive(false);
            }
            gameObject.SetActive(false);
        }
    }
}
