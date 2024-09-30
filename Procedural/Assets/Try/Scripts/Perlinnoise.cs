using UnityEngine;

public class Perlinnoise : MonoBehaviour
{
    public int width = 256;
    public int heigth = 256;

    public float offsetX = 100f;
    public float offsetY = 100f;

    public float scale = 20f;
    Renderer renderer = null;

    void Start()
    {
         renderer = GetComponent<Renderer>();
        offsetX = Random.Range(0f, 999999f);
        offsetY = Random.Range(0f, 999999f);
    }

    private void Update()
    {

        renderer.material.mainTexture = GenerateTexture();
    }

    private Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width,heigth);
        for(int x = 0; x < width; x++)
        {
            for (int y = 0; y < heigth; y++)
            {
                Color color = CalculateColor(x, y);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        return texture;
    }

    private Color CalculateColor(int x, int y)
    {
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / heigth * scale + offsetY;
        float sample = Mathf.PerlinNoise(xCoord, yCoord);
       return new Color(sample, sample, sample);
    }
}
