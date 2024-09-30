using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger2 : MonoBehaviour
{
	
       public void OnTriggerEnter2D(Collider2D collision)
       {
      
            UnityEngine.SceneManagement.SceneManager.LoadScene("Video", LoadSceneMode.Single);
            Debug.Log("Cambio de Escena");
        }
}
