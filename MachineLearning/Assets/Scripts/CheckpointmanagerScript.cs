using UnityEngine;

public class CheckpointmanagerScript : MonoBehaviour
{
    public int cPLeft = 0;
    int totalCP;
    private void Awake()
    {
        totalCP = transform.childCount;
        cPLeft = totalCP;
    }
    public void removeCheckpoint()
    {
        cPLeft -= 1;
    }

    public void restartCheckpoints()
    {
        cPLeft = totalCP;



        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<CheckpointScript>().GameReset();

        }

    }
    private void Update()
    {
        if (cPLeft <= 0)
        {
            restartCheckpoints();
        }

    }

    public void ResetGame()
    {
        restartCheckpoints();
    }

}
