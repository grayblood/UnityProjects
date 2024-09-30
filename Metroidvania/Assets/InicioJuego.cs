using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InicioJuego : MonoBehaviour
{

    public void Start(){
        UnityEngine.SceneManagement.SceneManager.LoadScene("Arnau", LoadSceneMode.Single);
    }

    public void Exit(){
         Application.Quit();
    }
}
