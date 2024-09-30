
using UnityEngine;
public class CarScript2 : MonoBehaviour
{
    //Rigidbody
    Rigidbody2D rb;
    public GameObject pruebaManager;

    //Layers
    public LayerMask pared;
    public LayerMask checkpoint;


    //variables globales
    Vector2 startPos;
    Quaternion rotationPosition;

    //var movement
    public float maxSpeed;
    public float acceleration;
    public float steering;
    Vector2 speed;
    float direction;
    float driftForce;

    //velocidad
    public float currentSpeed;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        startPos = transform.localPosition;
        rotationPosition = transform.localRotation;
        
    }

    public void ResetGame()
    {
        transform.localPosition = startPos;
        transform.localRotation = rotationPosition;

        speed = Vector2.zero;
        direction = 0;
        driftForce = 0;


    }

    
   
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {// Get input
        float h = -Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Calculate speed from input and acceleration (transform.up is forward)
        speed = transform.up * (v * acceleration);
        rb.AddForce(speed);

        // Create car rotation
        direction = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up));
        if (direction >= 0.0f)
        {
            rb.rotation += h * steering * (rb.velocity.magnitude / maxSpeed);
        }
        else
        {
            rb.rotation -= h * steering * (rb.velocity.magnitude / maxSpeed);
        }

        // Change velocity based on rotation
        driftForce = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.left)) * 2.0f;
        Vector2 relativeForce = Vector2.right * driftForce;
        Debug.DrawLine(rb.position, rb.GetRelativePoint(relativeForce), Color.green);
        rb.AddForce(rb.GetRelativeVector(relativeForce));

        // Force max speed limit
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
        currentSpeed = rb.velocity.magnitude;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pared")
        {
            // print("Crash");
            //print("giving not a reward");
            
            pruebaManager.GetComponent<PruebaManager>().Restart();
            ResetGame();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            collision.gameObject.GetComponent<CheckpointScript>().Touch();
            //print("giving reward");
            
        }
    }
}
