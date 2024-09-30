using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBoss : MonoBehaviour
{
    public float distanciamaxima;
    private Vector2 inicialpos;
    public float Speed;
    public Vector2 direction = Vector2.right;
    // Start is called before the first frame update
    void Start()
    {
        float angle = Vector2.Angle(direction, Vector2.right);
        if (direction.y < 0)
        {
            angle *= -1;
        }
        transform.localEulerAngles = new Vector3(0, 0, angle);
        GetComponent<Rigidbody2D>().velocity = transform.right * Speed;

    }

    // Update is called once per frame
    void Update()
    {
       // GetComponentInChildren<Transform>().position = gameObject.transform.position;
        
    }
}
