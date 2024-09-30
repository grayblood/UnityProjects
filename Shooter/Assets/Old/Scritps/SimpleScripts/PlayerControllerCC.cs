using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerCC : MonoBehaviour
{
    [SerializeField]
    private float m_speed = 3.0f;

    [SerializeField]
    private float m_rotateSpeed = 1.0f;

    [SerializeField]
    private float m_jumpHeight = 10.0f;

    [SerializeField]
    private float m_gravity = 9.8f;

    [SerializeField]
    private bool m_isGrounded;

 
    public Vector3 checkPointPosition;

    [SerializeField]
    private Vector3 m_movement;

    private bool m_warp = false;
    private Vector3 m_warpDestination;


    private CharacterController m_charController;
    private void Awake()
    {
        m_charController = GetComponent<CharacterController>();
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
    }

    private void LateUpdate()
    {
        if(m_warp)
        {
            transform.position = m_warpDestination;
            m_warp = false;
        }
    }

    private void HandleMovement()
    {
        Vector3 groundPlaneMovement = Vector3.zero;
        groundPlaneMovement.x = transform.forward.x * Input.GetAxis("Vertical");
        groundPlaneMovement.z = transform.forward.z * Input.GetAxis("Vertical");

        if (Input.GetAxis("Horizontal") != 0f)
        {
            //strafe
            if (Input.GetMouseButton(1))
            {
                groundPlaneMovement.x += transform.right.x * Input.GetAxis("Horizontal");
                groundPlaneMovement.z += transform.right.z * Input.GetAxis("Horizontal");
            }
            else
                transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * m_rotateSpeed);
        }

        groundPlaneMovement.Normalize();
        m_movement.x = groundPlaneMovement.x * m_speed;
        m_movement.z = groundPlaneMovement.z * m_speed;

        if (m_charController.isGrounded && Input.GetButtonDown("Jump"))
            m_movement.y = m_jumpHeight;

        //movement
        m_charController.Move(m_movement * Time.deltaTime);
        m_isGrounded = m_charController.isGrounded;

        //gravity
        if (m_charController.isGrounded)
            m_movement.y = -.5f;
        else
            m_movement.y -= m_gravity * Time.deltaTime;
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FanCollider")
        {
            m_gravity = -9.8f;
        }
        if (other.gameObject.tag == "ResPos")
        {
            print("Teleport player");
            m_warp = true;
            m_warpDestination = checkPointPosition;
            
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "FanCollider")
        {
            m_gravity = 9.8f;
        }
    }


}
