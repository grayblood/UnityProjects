using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorProceduralSimple : MonoBehaviour
{
    public GameObject[] blocks;
    private GameObject blockActual;
    public GameObject aigua;
    public GameObject joan;


    public int seed = 98734387;
    public int comunisme = 324232;

    //altura maxima
    public float amp = 50f;
    public float freq = 10f;



    // Start is called before the first frame update
    void Start()
    {
        gererateProceduralMap();
        
    }

    private void gererateProceduralMap()
    {
        Vector3 pos = this.transform.position;

        int fil = 100;
        int col = 100;

        //ens movem pel mapa com una matriu
        for(int x = 0; x<col; x++)
        {
            for(int z = 0; z < fil; z++)
            {


                float probJoan = Mathf.PerlinNoise((pos.x+x+comunisme)/freq, (pos.z + z + comunisme) / freq);


                float y = Mathf.PerlinNoise((pos.x + x +seed)/freq, (pos.z + z)/freq);
                y = y * amp;


                y -= 5;

                if(y > 70 * (amp / 100))
                {
                    blockActual = blocks[2];
                }else if(y > 40 * (amp / 100))
                {
                    blockActual = blocks[1];
                }
                else
                {
                    blockActual = blocks[0];
                }


                for(float i=y-3; i <= y; i++)
                {
                    GameObject newBlock = Instantiate(blockActual);
                    newBlock.transform.position = new Vector3(pos.x + x, i, pos.z + z);
                }
                for(float i = y; i < amp*30/100; i++)
                {
                    GameObject aguaPorFavor = Instantiate(aigua);
                    aguaPorFavor.transform.position= new Vector3(pos.x + x, i, pos.z + z);
                }

                if (probJoan>0.85f)
                {
                    GameObject newJoan = Instantiate(joan);
                    newJoan.transform.position = new Vector3(pos.x + x, y + 1, pos.z + z);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
