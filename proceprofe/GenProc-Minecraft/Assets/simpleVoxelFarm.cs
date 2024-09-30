using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleVoxelFarm : MonoBehaviour {


	private GameObject currentBlockType;
	public GameObject[] blockTypes;
    public GameObject water;

	[Tooltip("True for minecraft style voxels")]
	public bool SnapToGrid = true;

    public int seed = 35002852;
	public float amp = 10f;
	public float freq = 10f;


	private Vector3 myPos;

	void Start () {
        //el mapa de perlin siempre devuelve la misma posicion.
        //si tu quieres que deveulva algo distinto tendras que hacer un desfase. Ese defase es conocido como "seed" (semilla)
        print(Mathf.PerlinNoise(5f, 2f));
		generateTerrain ();
	}
	


	void generateTerrain(){

        //determinas posicion inicial
		myPos = this.transform.position;

        //dices numero de filas y columnas
		int cols = 100;
		int rows = 100;

        //recorro el mapa como si fuera una matriz.
		for (int x = 0; x < cols; x++) {


			for (int z = 0; z < rows; z++) {
			
                //si la x,z son muy pequeñas al dividirlas por una freq grande se van a quedar a 0 y luego se multiplicaran por una amplitud grande.
                //perlinnoise va a devolver la intensidad del mapa perlin de la posicion
				float y = Mathf.PerlinNoise ((myPos.x + x + seed) / freq, 
					          (myPos.z + z) / freq) * amp;


                if(seed % 9==0)
                {
                    print("wii sin");
                    y += Mathf.Sin(x);
                }
			
				if (SnapToGrid) {
                    //fuerzas a que la y sea un entero (las diferencias seran a nivel bloque, como si fuera minecraft)
					y = Mathf.Floor (y);
				}

                if (y > 3 * (amp / 4))
                    currentBlockType =
                    blockTypes[2];
                else if (y > 2 * (amp / 5))
                    currentBlockType = blockTypes[1];
                else
                    currentBlockType =
                    blockTypes[0];

                //si no hubiese el >=0, en los puntos que se queda justo a 0 habria un agujero
                for (float i = y; i >= y-5; i--)
                {
                    GameObject newBlock =
                  GameObject.Instantiate(currentBlockType);

                    newBlock.transform.position =
                        new Vector3(myPos.x + x, i, myPos.z + z);
                }
                for (float i = y; i < (amp/5); i++)
                {
                    GameObject waterBlock =
                  GameObject.Instantiate(water);

                    waterBlock.transform.position =
                        new Vector3(myPos.x + x, i, myPos.z + z);
                }



            }

		}

	
	}


}
