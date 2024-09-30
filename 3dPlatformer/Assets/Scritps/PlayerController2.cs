using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 3.0f;

    [SerializeField]
    private float rotateSpeed = 1.0f;

    [SerializeField]
    private float m_jumpforce = 10.0f;

    [SerializeField]
    private float m_gravity = 9.8f;

    [SerializeField]
    private bool m_isGrounded;

    [SerializeField]
    private Vector3 m_movement;

    private Rigidbody rb;

    public float rang;
    public LayerMask mask;
    public bool hasBeenHit;
    public bool tonto = false;
    public GameObject puerta;

    public Vector3 checkPointPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Renderer rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Specular");
        rend.material.SetColor("_Color", Color.blue);
        rend.material.SetColor("_SpecColor", Color.red);

        checkPointPosition = transform.position;

        print(checkPointPosition);
    }

    void Update()
    {
        HandleMovement();
        print(Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(KeyCode.E))
        {
            Usar();
        }
    }

    private void Usar()
    {
        RaycastHit hit;
        Debug.DrawRay(this.transform.position, this.transform.forward * rang, Color.magenta, 2f);
        if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, rang, mask))
        {
            print(hit.transform.name);
            if(hit.transform.tag == "Palanca")
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

    private void HandleMovement()
    {
        Vector3 movement = Vector3.zero;
        if (Input.GetAxis("Vertical") != 0f)
        {
            movement += transform.forward * Input.GetAxis("Vertical") * m_Speed;

        }
        if (Input.GetAxis("Horizontal") != 0f)
        {
            if (Input.GetMouseButton(1))
                movement += transform.right * Input.GetAxis("Horizontal") * m_Speed;
            else
                transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * rotateSpeed);
        }
        movement.Normalize();
        m_movement.x = movement.x * m_Speed;
        m_movement.z = movement.z * m_Speed;

        if (movement != Vector3.zero)
            rb.velocity = movement;

        if (m_isGrounded && Input.GetButtonDown("Jump"))
            m_movement.y = m_jumpforce;




        //gravity
        if (m_isGrounded)
            m_movement.y = -.5f;
        else
            m_movement.y -= m_gravity * Time.deltaTime;

        print(movement);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FanCollider")
        {
            rb.mass = -1f;
        }
        if (other.gameObject.tag == "ResPos")
        {

            transform.position = checkPointPosition;

        }
        if (other.gameObject.tag == "Ground")
        {
            m_isGrounded = true;

        }
        print(other.gameObject.tag);

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "FanCollider")
        {
            rb.mass = 1f;
        }
        if (other.gameObject.tag == "Ground")
        {
            m_isGrounded = false;
        }
    }


}
