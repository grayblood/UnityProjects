using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> SpawnPoints = new List<GameObject>();

    [System.Serializable]
    public class Wave
    {
        public List<GameObject> OneWave;
    }

    [System.Serializable]
    public class SpawnWaves{
        public List<Wave> ListOfWaves;
    }

    public SpawnWaves SpawnWavesList = new SpawnWaves();

    public int timeBetweenRounds = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
