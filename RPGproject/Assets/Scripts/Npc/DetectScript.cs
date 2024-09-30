using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DetectScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("He colisionado con algo");
        if (collision.tag == "Player") {
            Debug.Log("A PELEAR GUARRO");
            SceneManager.LoadScene("Combate", LoadSceneMode.Single);
        }
    }
}
