using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody m_rigidBody;
    [SerializeField]
    private float m_Speed = 3.0f;
    [SerializeField]
    private float rotateSpeed = 1.0f;

    [SerializeField]
    private float m_jumpforce = 10.0f;




    [SerializeField]
    private bool m_isGrounded;

    public Vector3 checkPointPosition;
    private Animator m_anim;
    [SerializeField]
    private GameObject cam;

    public float rang;
    public LayerMask mask;
    public bool hasBeenHit = false;
    public bool tonto = false;
    public GameObject puerta;
    public bool enable = true;

    private void Awake()
    {

        m_rigidBody = GetComponent<Rigidbody>();
        m_anim = GetComponent<Animator>();

        checkPointPosition = transform.position;

        print(checkPointPosition);


    }

    void Update()
    {
        HandleMovement();
        AnimationHandle();
        if (Input.GetKeyDown(KeyCode.E))
        {
            print("PUshing E");
            Usar();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.position = checkPointPosition;

            enable = true;

            m_anim.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            enable = false;
            m_anim.enabled = false;
        }
        if (Mathf.Abs(m_rigidBody.velocity.y) > 1.5f || Mathf.Abs(m_rigidBody.velocity.y) < -1.5f)
            m_anim.SetBool("onAir", true);
    }

    private void AnimationHandle()
    {

        if (Input.GetAxis("Vertical") != 0f)
        {
            if (Input.GetAxis("Vertical") >= 0f)
            {
                m_anim.SetBool("runForward", true);
                m_anim.SetBool("runBack", false);
            }
            else if (Input.GetAxis("Vertical") <= 0f)
            {
                m_anim.SetBool("runBack", true);
                m_anim.SetBool("runForward", false);
            }
        }
        else
        {
            m_anim.SetBool("runForward", false);
            m_anim.SetBool("runBack", false);
        }
        if (Input.GetAxis("Horizontal") != 0f)
        {
            if (Input.GetAxis("Horizontal") >= 0f)
            {
                m_anim.SetBool("runRight", true);
                m_anim.SetBool("runLeft", false);
            }
            else if (Input.GetAxis("Horizontal") <= 0f)
            {
                m_anim.SetBool("runLeft", true);
                m_anim.SetBool("runRight", false);
            }
        }
        else
        {
            m_anim.SetBool("runLeft", false);
            m_anim.SetBool("runRight", false);
        }



    }

    private void HandleMovement()
    {

        Vector3 movement = Vector3.zero;




        if (Input.GetAxis("Vertical") != 0f)
        {

            movement += cam.transform.forward * Input.GetAxis("Vertical") * m_Speed;
        }
        if (Input.GetAxis("Horizontal") != 0f)
        {

            movement += cam.transform.right * Input.GetAxis("Horizontal") * m_Speed;

        }

        if (movement != Vector3.zero)
        {
            m_rigidBody.velocity = new Vector3(movement.x, m_rigidBody.velocity.y, movement.z);
        }
        else
        {
            m_rigidBody.velocity = new Vector3(0f, m_rigidBody.velocity.y, 0f);
        }
        if (Input.GetButtonDown("Jump") && m_isGrounded)
        {

            m_isGrounded = false;
            m_rigidBody.AddForce(new Vector3(0, m_jumpforce, 0), ForceMode.Impulse);


        }


        if (movement.x != 0 || movement.z != 0)
        {
            Vector3 targetDir = movement; //Direction of the character

            targetDir.y = 0;
            Quaternion tr;
            if (Input.GetAxis("Vertical") > 0.1f)
            {
                tr = Quaternion.LookRotation(targetDir); //Rotation of the character to where it moves
            }
            else
            {
               tr = Quaternion.LookRotation(-targetDir);
            }
            transform.rotation = tr;
        }

    }

    private void Usar()
    {
        RaycastHit hit;
        Debug.DrawRay(this.transform.position, this.transform.forward * rang, Color.magenta, 2f);
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, rang, mask))
        {
            print(hit.transform.name);
            if (hit.transform.tag == "Palanca")
            {
                Lever lev = hit.collider.GetComponent<Lever>();
                Puerta pur = puerta.GetComponent<Puerta>();
                if (hasBeenHit)
                {
                    lev.MoverPalanca(hasBeenHit);
                    pur.MoverPuerta(hasBeenHit);
                    hasBeenHit = false;
                }
                else if (!hasBeenHit)
                {
                    lev.MoverPalanca(hasBeenHit);
                    pur.MoverPuerta(hasBeenHit);
                    hasBeenHit = true;
                }
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            m_isGrounded = true;

            m_anim.SetBool("onAir", false);

        }
        if (collision.gameObject.tag == "Projectil")
        {
            m_anim.enabled = false;
            enable = false;

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FanCollider")
        {
            m_rigidBody.useGravity = false;
            m_rigidBody.AddForce(new Vector3(0, 6, 0), ForceMode.Impulse);
        }
        if (other.gameObject.tag == "ResPos")
        {

            transform.position = checkPointPosition;

            enable = true;
            m_anim.enabled = true;


        }
        if (other.gameObject.tag == "FinMapa")
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Map02", LoadSceneMode.Single);

        }
        print(other.gameObject.tag);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "FanCollider")
        {
            m_rigidBody.useGravity = true;

        }


    }

}


