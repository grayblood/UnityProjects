using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpillSpawner : NetworkBehaviour
{
    private int manchasAct = 0;
    public int manchasMax = 3;

    public float tiempo = 10f;

    private Transform[] posiciones;
    List<Transform> unavailablepos;
    [ServerCallback]
    private void Start()
    {

        posiciones = new Transform[transform.childCount];
        unavailablepos = new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            posiciones[i] = transform.GetChild(i);
        }
        //print(posiciones);

        StartCoroutine(crearSpill());
    }

    [ServerCallback]
    private IEnumerator crearSpill()
    {
        while (true)
        {
            if (manchasAct < manchasMax)
            {
                Transform pos;

                    pos = posiciones[Random.Range(0, transform.childCount)];


                unavailablepos.Add(pos);
                manchasAct++;
                GameObject spill = Instantiate(NetworkManager.singleton.spawnPrefabs.Find(prefab => prefab.name == "Spill"), pos);
                NetworkServer.Spawn(spill);
            }

            yield return new WaitForSeconds(tiempo);
        }

    }
    [ServerCallback]
    public void removeMancha()
    {
        manchasAct--;
    }
}
