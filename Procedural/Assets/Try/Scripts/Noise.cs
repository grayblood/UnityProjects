using UnityEngine;

public class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight,int seed, float scale, int octaves, float persistance, float lacunarity,Vector2 offSet)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        Vector2[] octaveOffSets = new Vector2[octaves];
        /*
        for (int i = 0; i < octaves; i++)
        {

            octaveOffSets[i] = offSet;
        }
        */
        if (scale <= 0)
        {
            scale = 0.00001f;
        }
        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {

                float amplitud = 1;
                float frequencia = 1;
                float noiseHeight = 0;
                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = x / scale * frequencia * 1;
                    float sampleY = y / scale * frequencia * 1;
                    //float sampleX = x / scale * frequencia * octaveOffSets[i].x;
                    //float sampleY = y / scale * frequencia * octaveOffSets[i].y;
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2-1;
                    noiseHeight += perlinValue * amplitud;

                    amplitud *= persistance;
                    frequencia *= lacunarity;
                }
                if(noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }else if(noiseHeight< minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;
            }
        }
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }
                return noiseMap;
    }
}
