using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EstiloMenu
{
    horizontal, vertical
}

public class GunInventory : MonoBehaviour
{

    public GameObject armaActual;

    private int contadorDeArmas = 0;


    public List<string> armas = new List<string>();

    

    public float tiempoCambioDeArmas;

    public GameObject cam;
    public GameObject wS;
    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        wS = GameObject.FindGameObjectWithTag("WeaponSpawn");

        StartCoroutine("ArmaAlIniciar");

        if (armas.Count == 0)
            print("No hay armas");
    }

    /*
	*Spawn arma
	*/
    IEnumerator ArmaAlIniciar()
    {
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("Spawn", 0);
    }

   
    void Update()
    {

        tiempoCambioDeArmas += 1 * Time.deltaTime;
        if (tiempoCambioDeArmas > 1.2f && Input.GetKey(KeyCode.LeftShift) == false)
        {
            CrearArma();
        }

    }




    /*
	 *Cambio de armas
	 */
    void CrearArma()
    {


        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            tiempoCambioDeArmas = 0;

            contadorDeArmas++;
            if (contadorDeArmas > armas.Count - 1)
            {
                contadorDeArmas = 0;
            }
            StartCoroutine("Spawn", contadorDeArmas);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            tiempoCambioDeArmas = 0;

            contadorDeArmas--;
            if (contadorDeArmas < 0)
            {
                contadorDeArmas = armas.Count - 1;
            }
            StartCoroutine("Spawn", contadorDeArmas);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && contadorDeArmas != 0)
        {
            tiempoCambioDeArmas = 0;
            contadorDeArmas = 0;
            StartCoroutine("Spawn", contadorDeArmas);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && contadorDeArmas != 1)
        {
            tiempoCambioDeArmas = 0;
            contadorDeArmas = 1;
            StartCoroutine("Spawn", contadorDeArmas);
        }

    }


    IEnumerator Spawn(int _redniBroj)
    {


        if (armaActual)
        {

            yield return new WaitForSeconds(0.8f);//0.8 time to change weapon
            Destroy(armaActual);

            GameObject resource = (GameObject)Resources.Load(armas[_redniBroj].ToString());
            armaActual = (GameObject)Instantiate(resource, wS.transform.position, Quaternion.FromToRotation(Vector3.forward, cam.transform.forward), cam.transform);

        }
        else
        {
            GameObject resource = (GameObject)Resources.Load(armas[_redniBroj].ToString());
            armaActual = (GameObject)Instantiate(resource, wS.transform.position, Quaternion.FromToRotation(Vector3.forward, cam.transform.forward), cam.transform);

        }


    }






   

    public EstiloMenu menuEstilo = EstiloMenu.horizontal;

    public int spacing = 10;

    public Vector2 beginPosition;

    public Vector2 size;


   

    /*
	 * Muere jugador
	 */
    public void DeadMethod()
    {
        Destroy(armaActual);
        Destroy(this);
    }

    private float position_x(float var)
    {
        return Screen.width * var / 100;
    }
    private float position_y(float var)
    {
        return Screen.height * var / 100;
    }
    private float size_x(float var)
    {
        return Screen.width * var / 100;
    }
    private float size_y(float var)
    {
        return Screen.height * var / 100;
    }
    private Vector2 vec2(Vector2 _vec2)
    {
        return new Vector2(Screen.width * _vec2.x / 100, Screen.height * _vec2.y / 100);
    }

}
