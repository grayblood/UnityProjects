using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PruebaManager : MonoBehaviour
{
    public GameObject car;
    public GameObject checkpointM;
   
    public void Restart()
    {
        car.GetComponent<CarScript>().ResetGame();
        checkpointM.GetComponent<CheckpointmanagerScript>().ResetGame();

    }
}
