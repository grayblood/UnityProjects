using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeScript : MonoBehaviour
{
    [SerializeField]
    private ScriptableFloat m_hp;
    [SerializeField]
    private GameEvent m_OnPlayerHp;
    [SerializeField]
    private ScriptableFloat m_mp;
    [SerializeField]
    private GameEvent m_OnPlayerMp;
    public int m_atk = 1;
    public int m_def;
    public float m_speed;
    [SerializeField]
    private float m_slideForce;
    [SerializeField]
    private float m_slideDistance;
    [SerializeField]
    private float m_salto;
    [SerializeField]
    private int m_jumps;
    [SerializeField]
    private bool m_isGrounded;
    [SerializeField]
    private bool m_movent;
    [SerializeField]
    private float m_comboCounter;
    [SerializeField]
    private Vector3 m_initialPos;
    [SerializeField]
    private BoxCollider2D m_bc;
    [SerializeField]
    private Vector2 m_boxSize;
    [SerializeField]
    private GameObject shield;
    [SerializeField]
    private AudioSource swing;

    private SpriteRenderer m_sr;
    private Animator m_anim;
    //false right, true left
    private bool dir = false;


    // Start is called before the first frame update
    void Start()
    {
        m_sr = GetComponent<SpriteRenderer>();
        m_anim = GetComponent<Animator>();
        m_bc = GetComponent<BoxCollider2D>();
        m_boxSize = m_bc.size;
        m_isGrounded = false;
        m_comboCounter = 0;
        m_jumps = 2;
        m_movent = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Attack();
        Shield();
        if (m_isGrounded == true) Slide();
    }

    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            swing.Play();
            StopCoroutine("ComboTime");
            if (m_isGrounded == false)
            {
                m_anim.Play("AirAtk");
            }
            else
            {
                if (m_comboCounter == 0)
                {
                    m_anim.Play("BasicAtk");
                    m_comboCounter++;
                    StartCoroutine("ComboTime");
                }
                else if (m_comboCounter == 1)
                {
                    m_anim.Play("Combo1");
                    m_comboCounter++;
                    StartCoroutine("ComboTime");
                }
                else if (m_comboCounter == 2)
                {
                    m_anim.Play("Combo3");
                    m_comboCounter++;
                    StartCoroutine("ComboTime");
                }
                else if (m_comboCounter == 3)
                {
                    m_anim.Play("Combo2");
                    m_comboCounter++;
                    StartCoroutine("ComboTime");
                }
                else if (m_comboCounter == 4)
                {
                    m_anim.Play("Combo4");
                    m_comboCounter = 0;
                    StartCoroutine("ComboTime");
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.P) && m_isGrounded)
        {
            StartCoroutine("FlashStrike");
            m_movent = true;
        }
    }

    public void Shield() {
        if (m_mp.value == 0)
        {
            shield.SetActive(false);
        }
            if (Input.GetKey(KeyCode.J))
        {
            if (m_mp.value > 0) {
            shield.SetActive(true);
                m_mp.value--;
                m_OnPlayerMp.Raise();
            }
        }
        else if(m_mp.value < 500)
        {
            shield.SetActive(false);
            m_mp.value++;
            m_OnPlayerMp.Raise();
        }
    }

    IEnumerator FlashStrike()
    {
        m_initialPos = transform.position;
        m_anim.Play("FlashStrike");
        yield return new WaitForSeconds(1f);
        StartCoroutine(FlashStrikeSlide());
    }

    IEnumerator FlashStrikeSlide()
    {
        yield return new WaitForEndOfFrame();
        if (m_sr.flipX == false)
        {
            if (transform.position.x < m_initialPos.x + m_slideDistance)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(m_slideForce * 2 * Mathf.Sqrt(2) / 2, 0, 0);
                StartCoroutine(FlashStrikeSlide());
            }
            else
            {
                //GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                m_movent = false;
            }
        }
        else
        {
            if (transform.position.x > m_initialPos.x - m_slideDistance)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(-m_slideForce * 2 * Mathf.Sqrt(2) / 2, 0, 0);
                StartCoroutine(FlashStrikeSlide());
            }
            else
            {
               // GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                m_movent = false;
            }
        }
    }
    IEnumerator ComboTime()
    {
        yield return new WaitForSeconds(1f);
        m_comboCounter = 0;
    }

    void Slide()
    {
        if (Input.GetKeyUp(KeyCode.O))
        {
            m_anim.SetBool("sliding", false);
            m_bc.size = m_boxSize;
            m_movent = false;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            m_initialPos = transform.position;
            m_anim.Play("Slide");
            m_bc.size = new Vector2(m_bc.size.x, m_bc.size.y - 0.1f);
            m_movent = true;
        }
        else if (Input.GetKey(KeyCode.O))
        {
            if (m_sr.flipX == false)
            {
                if (transform.position.x < m_initialPos.x + m_slideDistance)
                    GetComponent<Rigidbody2D>().velocity = new Vector3(m_slideForce * Mathf.Sqrt(2) / 2, 0, 0);
                else
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                    m_anim.SetBool("sliding", false);
                    m_bc.size = m_boxSize;
                }
            }
            else
            {
                if (transform.position.x > m_initialPos.x - m_slideDistance)
                    GetComponent<Rigidbody2D>().velocity = new Vector3(-m_slideForce * Mathf.Sqrt(2) / 2, 0, 0);
                else
                {
                    GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
                    m_anim.SetBool("sliding", false);
                    m_bc.size = m_boxSize;
                }
            }
        }
    }

    public void Move()
    {
        if (m_movent == false)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                Jump();
            }
            if (m_anim.GetCurrentAnimatorStateInfo(0).IsName("BasicAtk") && m_isGrounded)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            }
          
            else if (Input.GetKey(KeyCode.D))
            {
                dir = false;
                m_anim.SetBool("run", true);
                GetComponent<Rigidbody2D>().velocity = new Vector3(m_speed , GetComponent<Rigidbody2D>().velocity.y, 0);
                if (m_sr.flipX == true)
                {
                    FlipHitboxesX(m_sr.flipX);
                }
                m_sr.flipX = false;

            }
            else if (Input.GetKey(KeyCode.A))
            {
                dir = true;
                m_anim.SetBool("run", true);
                GetComponent<Rigidbody2D>().velocity = new Vector3(-m_speed , GetComponent<Rigidbody2D>().velocity.y, 0);

                if (m_sr.flipX == false)
                {
                    FlipHitboxesX(m_sr.flipX);
                }
                m_sr.flipX = true;
            }
            else
            {
                m_anim.SetBool("run", false);
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, GetComponent<Rigidbody2D>().velocity.y, 0);
            }




        }
    }

    

    void Jump()
    {
        if (m_jumps > 0)
        {
            m_anim.SetTrigger("jump");
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(.0f, m_salto), ForceMode2D.Impulse);
            m_isGrounded = false;
            m_jumps--;
           
        }
        else 
        {
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            m_isGrounded = true;
           
            m_anim.SetTrigger("touchGround");
            m_jumps = 2;
        }

    }

    [SerializeField]
    private LayerMask m_collisionMask;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*List<Collider2D> colliders = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D();
        filter.useTriggers = true;
        filter.layerMask = m_collisionMask;
        int amount = collision.OverlapCollider(filter, colliders);
        if(colliders.Contains(shield.GetComponent<BoxCollider2D>()))
            Destroy(collision.gameObject);
            */
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Axe" || collision.gameObject.tag == "Bullet" || collision.gameObject.tag == "Spikes")
        {

            RecieveDamage(1);

        }
    }



    private void FlipHitboxesX(bool flip)
    {
        float right = 0.1f;
        float left = -0.420f;
        Vector3 position = transform.GetChild(1).transform.localPosition;
        if (flip) {
            transform.GetChild(1).transform.localPosition = new Vector3(right, position.y, position.z);
        } else {
            transform.GetChild(1).transform.localPosition = new Vector3(left, position.y, position.z);
        }
        
    }
    private void RecieveDamage(int dmg)
    {
        Vector2 impulse = new Vector2(-30.0f, 10.0f);
        if (!dir)
            impulse.x *= -1;

        GetComponent<Rigidbody2D>().AddForce(impulse, ForceMode2D.Impulse);
        
        m_hp.value -= dmg;
        m_OnPlayerHp.Raise();
        Debug.Log("Vida Ruby: " + m_hp.value);

        if (m_hp.value <= 0)
        {

            Debug.Log("Has muerto");

            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
            m_hp.value = 10;
        }
    }
}



