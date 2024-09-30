using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terrainnoise : MonoBehaviour
{


    public int width = 256;
    public int heigth = 256;

    public float offsetX = 100f;
    public float offsetY = 100f;

    public int octaves = 10;
    public float persistance = 100f;
    public float lacunarity = 100f;

    public int depth = 20;
    public float scale = 20f;
    Terrain terrain = null;

    void Start()
    {
        terrain = GetComponent<Terrain>();
        
        offsetX = Random.Range(0f, 999999f);
        offsetY = Random.Range(0f, 999999f);
    }

    private void Update()
    {

        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }

    private TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        terrainData.size = new Vector3(width, depth, heigth);

        terrainData.SetHeights(0, 0, GenerateHeights());

        return terrainData;
    }

    private float[,] GenerateHeights()
    {
        float[,] heights = new float[width, heigth];
        for(int x = 0; x < width; x++)
        {
            for (int y = 0; y < heigth; y++)
            {
                
                for (int i = 0; i < octaves; i++)
                {
                    heights[x, y] = CalculateHeight(x, y);
                }
            }
        }
        return heights;
    }

    float CalculateHeight(int x,int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / heigth * scale + offsetY;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }

}
