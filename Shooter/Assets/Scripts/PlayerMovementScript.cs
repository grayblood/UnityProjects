using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovementScript : MonoBehaviour
{
    Rigidbody rb;


    public float currentSpeed;

    [HideInInspector] public Transform cameraMain;

    public float jumpForce = 3;

    [HideInInspector] public Vector3 cameraPosition;

    public GameObject pies;

    [HideInInspector] public float vM;
    [HideInInspector] public float hM;

    bool grounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cameraMain = transform.Find("Main Camera").transform;
        bulletSpawn = cameraMain.Find("BulletSpawn").transform;
        //ignoreLayer = 1 << LayerMask.NameToLayer("Player");


    }

    public int Velocidad = 10;

    public float velocidadDeDesaceleracion = 30.0f;


    public float velocidadDeAceleracion =500.0f;

    private Vector3 slowdownV;
    private Vector2 horizontalMovement;
    void HandleInput()
    {
        hM = Input.GetAxis("Horizontal");
        vM = Input.GetAxis("Vertical");
    }

    void Update()
    {
        grounded = RaycastSuelo();
        HandleInput();
        
        
    }
    void FixedUpdate()
    {
        PlayerMovementLogic();
        Jumping();
        Crouching();
    }

    void PlayerMovementLogic()
    {
        currentSpeed = rb.velocity.magnitude;
        horizontalMovement = new Vector2(rb.velocity.x, rb.velocity.z);
        if (horizontalMovement.magnitude > Velocidad)
        {
            horizontalMovement = horizontalMovement.normalized;
            horizontalMovement *= Velocidad;
        }
        rb.velocity = new Vector3(horizontalMovement.x, rb.velocity.y, horizontalMovement.y);
        if (grounded)
        {
            rb.velocity = Vector3.SmoothDamp(rb.velocity,
                new Vector3(0, rb.velocity.y, 0),
                ref slowdownV,
                velocidadDeDesaceleracion);
        }
        
        if (grounded)
        {

            rb.AddRelativeForce(hM * velocidadDeAceleracion , 0, vM * velocidadDeAceleracion );
        }
        else
        {
            rb.AddRelativeForce(hM * velocidadDeAceleracion / 2 , 0, vM * velocidadDeAceleracion / 2 );

        }

        if (hM != 0 || vM != 0)
        {
            velocidadDeDesaceleracion = 1000f;
        }
        else
        {
            velocidadDeDesaceleracion = 1000f;
        }
        
    }

    void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            grounded = false;
            print("Estoy saltando!");

        }
    }


    
    private bool RaycastSuelo()
    {
        RaycastHit groundedInfo;
        bool tocado = Physics.Raycast(GetComponent<Collider>().bounds.center, Vector3.down, out groundedInfo, GetComponent<Collider>().bounds.extents.y*1.2f, ~ignoreLayer);
       // Debug.DrawRay(GetComponent<Collider>().bounds.min + transform.up * 0.1f, -transform.up * 0.1f, Color.blue, 0.1f);
        
        if(tocado)
            Debug.DrawLine(GetComponent<Collider>().bounds.min, groundedInfo.point, Color.red, 0.1f);
        return tocado;
    }


    void Crouching()
    {
        if (Input.GetKey(KeyCode.C))
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 0.6f, 1), Time.deltaTime * 15);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), Time.deltaTime * 15);

        }
    }




    



    private string currentWeapo;
    //ignorar Jugador
    public LayerMask ignoreLayer;


    public Transform bulletSpawn;




    public GameObject bloodEffect;//blood effect prefab;

    void InstantiateBlood(RaycastHit _hitPos)
    {


        if (currentWeapo == "gun")
        {
            if (bloodEffect)
                Instantiate(bloodEffect, _hitPos.point, Quaternion.identity);
            else
                print("Missing blood effect prefab in the inspector.");
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Slope")
        {
            print("entro");
            rb.AddForce(new Vector3(0, -5f, 0), ForceMode.VelocityChange);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == "Slope")
        {
            print("me quedo");
            rb.AddForce(new Vector3(0, -2f, 0), ForceMode.VelocityChange);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Slope")
        {
            print("salgo");
            rb.AddForce(new Vector3(0, -5f, 0), ForceMode.VelocityChange);
        }
    }
    private GameObject myBloodEffect;

}
