using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    private float vidaMax = 250;
    public float vida;
    public int bombaMax = 3;
    public int bomba = 3;
    float force = 0;
    float experience = 0;
    public bool bombactiva = false;
    public float inventime = 2f;
    float tiempo;
    public bool invencible = false;
    public float coliborde = .5f;
    private float speed = 5;


    public bool shooting;

    public Transform Gunholder1;
    public Transform Gunholder2;

   

    //HP
    public Image circleBar;
    public Image extraBar;
    public float circlePercent = 0.3f;
    private const float circleFillAmount = 0.75f;


    private void Awake()
    {
        shooting = false;
        vida += vidaMax;
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        tiempo = inventime;
    }

    private void Update()
    {
        CircleFill();
        ExtraFill();




        Vector2 movement = new Vector2();
        if (Input.GetKey(KeyCode.W))
        {
            movement.y = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement.y = -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement.x = -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement.x = 1;
        }
        if (Input.GetKey(KeyCode.E))
        {
            shooting = true;
        }
        else
        {
            shooting = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bomba -= 1;
            bombactiva = true;
            invencible = true;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {

            coliborde = .3f;
            speed = 2f;
        }
        else
        {
            
            coliborde = .5f;
            speed = 5f;
        }
        GetComponent<Rigidbody2D>().velocity = movement.normalized * speed;
        if (invencible == true)
        {
            print(tiempo);
            if (tiempo > 0)
            {
                tiempo -= Time.deltaTime;
            }
            else if (tiempo <= 0)
            {
                invencible = false;
                tiempo = inventime;
            }
        }
        transform.localScale = new Vector3(coliborde, coliborde, 1);

    }
    public void dañar()
    {
        bombactiva = true;
        //print("Me hacen daño");
        vida -= 25;
        invencible = true;
        if(vida <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void CircleFill()
    {
        //Funcionalidad de la zona circular de el hp
        float healthPercent = vida / vidaMax;
        float circleFill = healthPercent / circlePercent;
        circleFill *= circleFillAmount;
        circleFill = Mathf.Clamp(circleFill, 0, circleFillAmount);
        circleBar.fillAmount = circleFill;
        //print("Hay que llenar: " + circleFill);
    }

    void ExtraFill()
    {
        //Funcionalidad de la parte recta del hp
        float circleAmount = circlePercent * vidaMax;
        float extraHealth = vida - circleAmount;
        float extraTotalHealth = vidaMax - circleAmount;

        float extraFill = extraHealth / extraTotalHealth;
        extraFill = Mathf.Clamp(extraFill, 0, 1);
        extraBar.fillAmount = extraFill;
    }
    

}

