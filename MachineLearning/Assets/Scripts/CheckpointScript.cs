using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    public GameObject cPManager;
    private void Awake()
    {
        gameObject.SetActive(true);
    }
    public void Touch()
    {
        cPManager.GetComponent<CheckpointmanagerScript>().removeCheckpoint();
        gameObject.SetActive(false);
        
    }
    public void GameReset()
    {
        gameObject.SetActive(true);
    }
}
