using UnityEngine;

public class BlockWorld : MonoBehaviour
{
    public GameObject[] bloquets;
    private GameObject bloquet;

    public GameObject aigua;

    public GameObject redonda;

    public int semilla;

    public float prob;
    public float amplitud = 50f;
    public int frequencia = 10;
    public bool aiguant = false;

    public int size = 50;

    private void Start()
    {
        GenerateBlocks();
    }

    private void GenerateBlocks()
    {
        Vector3 pos = this.transform.position;



        for (int x = 0; x < size; x++)
        {
            for (int z = 0; z < size; z++)
            {
                float probs = Mathf.PerlinNoise((pos.x + x + prob) / frequencia, (pos.z + z + prob) / frequencia);
                float y = Mathf.PerlinNoise((pos.x + x + semilla) / frequencia, (pos.z + z) / frequencia);

                y = y * amplitud;



                if (y > 40 * (amplitud / 100))
                {
                    bloquet = bloquets[1];
                }
                else
                {
                    bloquet = bloquets[0];
                }
                for (float i = y - 3; i <= y; i++)
                {
                    GameObject newBlock = Instantiate(bloquet);
                    newBlock.transform.position = new Vector3(pos.x + x, i, pos.z + z);
                }
                if (!aiguant)
                {
                    for (float i = y; i < amplitud * 10 / 100; i++)
                    {
                        GameObject aguaPorFavor = Instantiate(aigua);
                        aguaPorFavor.transform.position = new Vector3(pos.x + x, i, pos.z + z);
                    }
                }

                if (prob > 0.85f)
                {
                    GameObject redondo = Instantiate(redonda);
                    redondo.transform.position = new Vector3(pos.x + x, y + 1, pos.z + z);
                }

            }
        }
    }
}
