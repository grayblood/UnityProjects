 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ProceduralGeneration : MonoBehaviour
{
    [Header("GenerarTerreno")]
    [SerializeField] int width, height;
    [SerializeField] float smoothness;
    [SerializeField] float seed;

    [Header("Generacion Cueva")]
    [Range(0,1)]
    [SerializeField] float modificador;

    [Header("Tile")]
    [SerializeField] TileBase TerrenoTileBase;
    [SerializeField] TileBase TerrenoNevadoTileBase;
    [SerializeField] TileBase TerrenoRelleno;
    [SerializeField] TileBase AguaTileBase;
    [SerializeField] TileBase CuevaTileBase;
    [SerializeField] Tilemap TerrenoTilemap;
    [SerializeField] Tilemap CuevaTilemap;

    [Header("Generacio de Objectes")]
    [SerializeField] GameObject Copium;
    [SerializeField] int FrequenciaDelCopium;
    [SerializeField] int NivelDeCopium;

    [SerializeField] GameObject Diamante;
    [SerializeField] int FrequenciaDelDiamante;
    [SerializeField] int NivelDeDiamante;

    int[,] map;

    // Start is called before the first frame update
    void Start()
    {
        Generar();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Generar();
        }
    }

    void Generar()
    {
        LimpiaMapa();
        map = GenerarArray(width, height, true);
        map = TerrenoGenerar(map);
        RenderMap(map, TerrenoTilemap, TerrenoTileBase, TerrenoNevadoTileBase, AguaTileBase, TerrenoRelleno, CuevaTilemap, CuevaTileBase);

    }

    public int [,] GenerarArray (int width, int height, bool empty)
    {
        int[,] map = new int[width, height];
        for(int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = empty ? 0 : 1;
            }
        }
        return map;
    }

    public int [,] TerrenoGenerar(int[,] map)
    {
        int AlturaPerling;
        for (int x = 0; x < width; x++)
        {
            AlturaPerling = Mathf.RoundToInt(Mathf.PerlinNoise(x/smoothness,seed)*height);
            for (int y = 0; y < AlturaPerling; y++)
            {
                //map[x, y] = 1;
                int ValorCueva = Mathf.RoundToInt(Mathf.PerlinNoise((x * modificador) + seed, (y * modificador) + seed));
                map[x, y] = (ValorCueva == 1) ? 2 : 1;
            }
        }
        return map;
    }

    public void RenderMap(int[,] map, Tilemap TerrenoTilemap, TileBase TerrenoTileBase, TileBase TerrenoNevadoTileBase, TileBase AguaTileBase, TileBase TerrenoRelleno, Tilemap CuevaTilemap, TileBase CuevaTileBase)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 pos = new Vector3(x, y, 1);

                float ProbabiltatCopium = Mathf.PerlinNoise((pos.x + x +  NivelDeCopium) / FrequenciaDelCopium, (pos.z + y + NivelDeCopium) / FrequenciaDelCopium);
                float ProbabiltatDiamant = Mathf.PerlinNoise((pos.x + x + NivelDeDiamante) / FrequenciaDelDiamante, (pos.z + y + NivelDeDiamante) / FrequenciaDelDiamante);

                if (map[x,y] == 1 && map[x, y+1] == 0  && y <= 60  || map[x, y] == 1 && map[x, y + 1] == 2 && y <= 60)
                {
                    Vector3Int posicion = new Vector3Int(x, y, 0);
                    TerrenoTilemap.SetTile(posicion, TerrenoTileBase);
                    if (ProbabiltatCopium > 0.85f)
                    {
                        print("Generado Copium");
                        GameObject NewCopium = Instantiate(Copium);
                        NewCopium.transform.position = TerrenoTilemap.GetCellCenterWorld(posicion+Vector3Int.up);
                    }
                }
                else if (map[x, y] == 2 && y <= 60 && y > 10)
                {
                    CuevaTilemap.SetTile(new Vector3Int(x, y, 0), CuevaTileBase);
                }

                if (map[x, y] == 2 && y <= 10)
                {
                    CuevaTilemap.SetTile(new Vector3Int(x, y, 0), AguaTileBase);
                }

                if (map[x, y] == 1 && y <= 10 && map[x, y +1] != 2)
                {
                    Vector3Int posicion = new Vector3Int(x, y, 0);
                    if (ProbabiltatDiamant > 0.7f)
                    {
                        print("Generado Diamante");
                        GameObject NewDiamante = Instantiate(Diamante);
                        NewDiamante.transform.position = TerrenoTilemap.GetCellCenterWorld(posicion);
                    }
                }

                if (map[x, y] == 2 && y > 60 && map[x, y + 1] != 0)
                {
                    TerrenoTilemap.SetTile(new Vector3Int(x, y, 0), TerrenoRelleno);
                }

                if (map[x, y] == 1 && map[x, y + 1] != 2 && map[x, y + 1] != 0)
                {
                    TerrenoTilemap.SetTile(new Vector3Int(x, y, 0), TerrenoRelleno);
                }

                if (map[x, y] == 1 && map[x, y + 1] == 2 && y > 60 || map[x, y] == 1 && map[x, y + 1] == 0 && y > 60)
                {
                    TerrenoTilemap.SetTile(new Vector3Int(x, y, 0), TerrenoNevadoTileBase);
                }
            }
        }
    }

    void LimpiaMapa()
    {
        TerrenoTilemap.ClearAllTiles();
        CuevaTilemap.ClearAllTiles();
    }
}
