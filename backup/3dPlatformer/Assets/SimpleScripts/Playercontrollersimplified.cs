using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontrollersimplified : MonoBehaviour
{

    public float m_Speed;
    public float rotateSpeed;
    // private Rigidbody m_rigidBody;
    CharacterController m_characterC;
    void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("Specular");
        rend.material.SetColor("_Color", Color.blue);
        rend.material.SetColor("_SpecColor", Color.red);
       
    }
    private void Awake()
    {
        // m_rigidBody = GetComponent<Rigidbody>();
        m_characterC = GetComponent<CharacterController>();
    }


    void Update()
    {

        Vector3 movement = Vector3.zero;
        if (Input.GetAxis("Vertical") != 0f)
            movement += transform.forward * Input.GetAxis("Vertical") * m_Speed;

        if (Input.GetAxis("Horizontal") != 0f)
        {
            if (Input.GetMouseButton(1))
                movement += transform.right * Input.GetAxis("Horizontal") * m_Speed;
            else
                transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * rotateSpeed);
        }

        m_characterC.SimpleMove(movement);
    }


    //m_rigidBody
    /* 
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

    if (movement != Vector3.zero)
        m_rigidBody.velocity = movement;

    if (Input.GetButtonDown("Fire1"))
    {
        print("pinyauuuuu");
    }
}
*/
}
