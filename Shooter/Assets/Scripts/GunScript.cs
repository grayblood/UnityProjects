using System.Collections;
using TMPro;
using UnityEngine;

public enum ModoDisparo
{
    nonautomatic, automatic
}
public class GunScript : MonoBehaviour
{

    public ModoDisparo modoActual;

    [HideInInspector]
    public MouseLookScript mls;


    public int walkingSpeed = 5;


    public int runningSpeed = 13;

    public float dmg = 1;

    public bool armaProjectil = false;

    public float inventarioBalas = 20;

    public float cargadorMaximo = 5;

    public float balasEnArma = 5; //añadir recarga de 1 en 1 hasta el maximo

    public float balasPorRecarga = 5;
    [HideInInspector]
    private Transform player;
    [HideInInspector]
    public GameObject wS;

    public GameObject playerGO;

    [HideInInspector]
    private PlayerMovementScript pmS;

    [SerializeField]
    public Vector3 posicionArmaCadera;

    [SerializeField]
    public Vector3 posicionArmaApuntado;
    [SerializeField]
    public bool a = false;

    [SerializeField]
    public bool autoreload = true;

    public Transform mainCamera;


    public float sensNoApuntando = 10;

    public float sensApuntando = 5;

    public float sensCorriendo = 4;


    public bool reloading;



    public float precisionArma;

    public GameObject bulletSpawnPlace;

    public GameObject projectil;

    public float velocidadDeDisparo;
    private float cadencia;



    void Awake()
    {
        
        mls = playerGO.GetComponent<MouseLookScript>();
        print(mls);
        player = mls.transform;
   
        mainCamera = mls.myCamera;


        pmS = player.GetComponent<PlayerMovementScript>();

        if (armaProjectil)
            bulletSpawnPlace = GameObject.FindGameObjectWithTag("ProjectilSpawn");
        else
            bulletSpawnPlace = GameObject.FindGameObjectWithTag("BulletSpawn");

        wS = GameObject.FindGameObjectWithTag("WeaponSpawn");

        
        PositionYRotacionGun();
    }







    /*
	Update loop calling for methods that are descriped below where they are initiated.
	*/
    void Update()
    {


        Darsensibilidad();
        Aim();


        Shooting();


        Sprint();

        CrossHairExpansionWhenWalking();
        if (Input.GetKeyDown(KeyCode.R) && pmS.Velocidad < runningSpeed && !reloading)
        {
         
            StartCoroutine("Reload");
            
        }

    }





    /*
	 * Used to give our main camera different sensivity options for each gun.
	 */
    void Darsensibilidad()
    {
        mls.mouseSensitvity_notAiming = sensNoApuntando;
        mls.mouseSensitvity_aiming = sensApuntando;

    }
    void Aim()
    {


        if (Input.GetAxisRaw("Fire2") == 1)
        {
            //print(Input.GetAxisRaw("Fire2"));
            //Aim
            transform.localPosition = posicionArmaApuntado;
        }
        else if (a)
        {
            transform.localPosition = posicionArmaApuntado;
        }
        else
        {
            transform.position = wS.transform.position;
        }

    }

    /*
     * Used to expand position of the crosshair or make it dissapear when running
     */
    void CrossHairExpansionWhenWalking()
    {

        if (player.GetComponent<Rigidbody>().velocity.magnitude > 1 && Input.GetAxis("Fire1") == 0)
        {

            expandValues_crosshair += new Vector2(20, 40) * Time.deltaTime;
            if (player.GetComponent<PlayerMovementScript>().Velocidad < runningSpeed)
            {
                expandValues_crosshair = new Vector2(Mathf.Clamp(expandValues_crosshair.x, 0, 10), Mathf.Clamp(expandValues_crosshair.y, 0, 20));
                fadeout_value = Mathf.Lerp(fadeout_value, 1, Time.deltaTime * 2);
            }
            else
            {
                fadeout_value = Mathf.Lerp(fadeout_value, 0, Time.deltaTime * 10);
                expandValues_crosshair = new Vector2(Mathf.Clamp(expandValues_crosshair.x, 0, 20), Mathf.Clamp(expandValues_crosshair.y, 0, 40));
            }
        }
        else
        {
            expandValues_crosshair = Vector2.Lerp(expandValues_crosshair, Vector2.zero, Time.deltaTime * 5);
            expandValues_crosshair = new Vector2(Mathf.Clamp(expandValues_crosshair.x, 0, 10), Mathf.Clamp(expandValues_crosshair.y, 0, 20));
            fadeout_value = Mathf.Lerp(fadeout_value, 1, Time.deltaTime * 2);

        }

    }

    /*Sprint*/
    void Sprint()
    {
        if (Input.GetAxis("Vertical") > 0 && Input.GetAxisRaw("Fire2") == 0 && Input.GetAxisRaw("Fire1") == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (pmS.Velocidad == walkingSpeed)
                {
                    pmS.Velocidad = runningSpeed;

                }
                else
                {
                    pmS.Velocidad = walkingSpeed;
                }
            }
        }
        else
        {
            pmS.Velocidad = walkingSpeed;
        }

    }


 

    void PositionYRotacionGun()
    {
        transform.position = wS.transform.position;
        transform.rotation = wS.transform.rotation;

    }




    void Shooting()
    {


        if (modoActual == ModoDisparo.nonautomatic)
        {
            if (Input.GetButtonDown("Fire1"))
            {

                ShootMethod();

            }


        }
        if (modoActual == ModoDisparo.automatic)
        {
            if (Input.GetButton("Fire1"))
            {

                ShootMethod();
            }


        }

        cadencia -= velocidadDeDisparo * Time.deltaTime;
    }






    private void ShootMethod()
    {
        StopAllCoroutines();
        if (cadencia <= 0 && !reloading && pmS.Velocidad < runningSpeed)
        {
            
            if (balasEnArma > 0)
            {
               


                if (projectil)
                {
                   
                    Instantiate(projectil, bulletSpawnPlace.transform.position, bulletSpawnPlace.transform.rotation);

                    if (projectil.GetComponent<Rigidbody>() == true)
                    {
                        
                        projectil.GetComponent<Rigidbody>().velocity = transform.forward * 25f;

                    }

                }
                else
                {
                    print("Missing the bullet prefab");
                }


                cadencia = 1;
                print("elimino bala del cargador");
                balasEnArma -= 1;

            }
            else
            {

               
                StartCoroutine("Reload");

            }

        }

    }




    public float reloadChangeBulletsTime;
   
    IEnumerator Reload()
    {



        yield return new WaitForSeconds(reloadChangeBulletsTime - 0.5f);
        if (pmS.Velocidad != runningSpeed)
        {
            if (inventarioBalas < 1)
            {
                print("Out OF BULLETS");
            }
            else
            {
                

                if (balasEnArma < cargadorMaximo)
                {
                    
                    float balasFaltantes = cargadorMaximo - balasEnArma;
                    print(balasFaltantes);
                    if (balasFaltantes <= inventarioBalas)
                    {
                        print(balasEnArma + balasPorRecarga);
                        if (balasFaltantes >= balasPorRecarga && balasPorRecarga <= cargadorMaximo)
                        {
                            balasEnArma += balasPorRecarga;
                            inventarioBalas -= balasPorRecarga;
                        }
                        else
                        {
                            balasEnArma += balasFaltantes;
                            inventarioBalas -= balasFaltantes;
                        }
                        

                    }
                }


            }
            if(balasEnArma != cargadorMaximo && autoreload)
            {

                StartCoroutine("Reload");
            }
        }
        else
        {


            print("interrupted");
        }


    }



    public TextMeshPro HUD_bullets;
    void OnGUI()
    {
        if (!HUD_bullets) 
        {
            try
            {
                HUD_bullets = GameObject.Find("HUD_bullets").GetComponent<TextMeshPro>();
                
            }
            catch (System.Exception ex)
            {
                print("Couldnt find the HUD_Bullets ->" + ex.StackTrace.ToString());
            }
        }
        if (mls && HUD_bullets)
            HUD_bullets.text = inventarioBalas.ToString() + " - " + balasEnArma.ToString();

      
        DrawCrosshair();
    }

    [Header("Crosshair properties")]
    public Texture horizontal_crosshair, vertical_crosshair;
    public Vector2 top_pos_crosshair, bottom_pos_crosshair, left_pos_crosshair, right_pos_crosshair;
    public Vector2 size_crosshair_vertical = new Vector2(1, 1), size_crosshair_horizontal = new Vector2(1, 1);
    [HideInInspector]
    public Vector2 expandValues_crosshair;
    private float fadeout_value = 1;
    /*
	 * Drawing the crossHair.
	 */
    void DrawCrosshair()
    {
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, fadeout_value);
        if (Input.GetAxis("Fire2") == 0)
        {//if not aiming draw
            GUI.DrawTexture(new Rect(vec2(left_pos_crosshair).x + position_x(-expandValues_crosshair.x) + Screen.width / 2, Screen.height / 2 + vec2(left_pos_crosshair).y, vec2(size_crosshair_horizontal).x, vec2(size_crosshair_horizontal).y), vertical_crosshair);//left
            GUI.DrawTexture(new Rect(vec2(right_pos_crosshair).x + position_x(expandValues_crosshair.x) + Screen.width / 2, Screen.height / 2 + vec2(right_pos_crosshair).y, vec2(size_crosshair_horizontal).x, vec2(size_crosshair_horizontal).y), vertical_crosshair);//right

            GUI.DrawTexture(new Rect(vec2(top_pos_crosshair).x + Screen.width / 2, Screen.height / 2 + vec2(top_pos_crosshair).y + position_y(-expandValues_crosshair.y), vec2(size_crosshair_vertical).x, vec2(size_crosshair_vertical).y), horizontal_crosshair);//top
            GUI.DrawTexture(new Rect(vec2(bottom_pos_crosshair).x + Screen.width / 2, Screen.height / 2 + vec2(bottom_pos_crosshair).y + position_y(expandValues_crosshair.y), vec2(size_crosshair_vertical).x, vec2(size_crosshair_vertical).y), horizontal_crosshair);//bottom
        }

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
