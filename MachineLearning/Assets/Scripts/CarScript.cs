using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
public class CarScript : Agent
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
    public float maxSpeed = 3f;
    public float acceleration = 1f;
    public float steering = 1f;
    Vector2 speed;
    float direction;
    float driftForce;

    //velocidad
    public float currentSpeed;

    //Tamaño del rayo
    [SerializeField]
    public float raysizeW = 0.6f;
    public float raysizeC = 0.6f;

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
    public override void OnEpisodeBegin()
    {
        pruebaManager.GetComponent<PruebaManager>().Restart();
    }
    public override void CollectObservations(VectorSensor sensor)
    {
        DetectarParedes(sensor);
        DetectarCheckpoint(sensor);

    }

    public override void OnActionReceived(ActionBuffers actionBuffers)
    {
        ActionSegment<int> discreteActions = actionBuffers.DiscreteActions;
        float h = discreteActions[0];
        float v = discreteActions[1];

        if (h == 2)
            h = -1;
        if (v == 2)
            v = -1;

        print("h " + h + " v " + v);
        HandleMovement(h, v);



    }
    private void Update()
    {
        RequestDecision();
    }

    private void HandleMovement(float h, float v)
    {
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


    private void DetectarCheckpoint(VectorSensor sensor)
    {
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, transform.up, raysizeC, checkpoint);
        Debug.DrawRay(transform.position, transform.up, Color.red);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, -transform.up, raysizeC, checkpoint);
        Debug.DrawRay(transform.position, -transform.up, Color.red);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, -transform.right, raysizeC, checkpoint);
        Debug.DrawRay(transform.position, -transform.right, Color.red);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, transform.right, raysizeC, checkpoint);
        Debug.DrawRay(transform.position, transform.right, Color.red);
        RaycastHit2D hitUpLeft = Physics2D.Raycast(transform.position, transform.up + -transform.right, raysizeC, checkpoint);
        Debug.DrawRay(transform.position, transform.up + -transform.right, Color.blue);
        RaycastHit2D hitDownLeft = Physics2D.Raycast(transform.position, -transform.up + -transform.right, raysizeC, checkpoint);
        Debug.DrawRay(transform.position, -transform.up + -transform.right, Color.blue);
        RaycastHit2D hitUpRight = Physics2D.Raycast(transform.position, transform.up + transform.right, raysizeC, checkpoint);
        Debug.DrawRay(transform.position, transform.up + transform.right, Color.blue);
        RaycastHit2D hitDownRight = Physics2D.Raycast(transform.position, -transform.up + transform.right, raysizeC, checkpoint);
        Debug.DrawRay(transform.position, -transform.up + transform.right, Color.blue);




        if (hitUp.collider != null)
            sensor.AddObservation(hitUp.distance);
        else
            sensor.AddObservation(-1);

        if (hitDown.collider != null)
            sensor.AddObservation(hitDown.distance);
        else
            sensor.AddObservation(-1);

        if (hitRight.collider != null)
            sensor.AddObservation(hitRight.distance);
        else
            sensor.AddObservation(-1);

        if (hitLeft.collider != null)
            sensor.AddObservation(hitLeft.distance);
        else
            sensor.AddObservation(-1);

        if (hitUpLeft.collider != null)
            sensor.AddObservation(hitUpLeft.distance);
        else
            sensor.AddObservation(-1);

        if (hitDownLeft.collider != null)
            sensor.AddObservation(hitDownLeft.distance);
        else
            sensor.AddObservation(-1);

        if (hitUpRight.collider != null)
            sensor.AddObservation(hitUpRight.distance);
        else
            sensor.AddObservation(-1);

        if (hitDownRight.collider != null)
            sensor.AddObservation(hitDownRight.distance);
        else
            sensor.AddObservation(-1);
    }

    private void DetectarParedes(VectorSensor sensor)
    {
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, transform.up, raysizeW, pared);
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, -transform.up, raysizeW, pared);
        RaycastHit2D hitLeft = Physics2D.Raycast(transform.position, -transform.right, raysizeW, pared);
        RaycastHit2D hitRight = Physics2D.Raycast(transform.position, transform.right, raysizeW, pared);
        RaycastHit2D hitUpLeft = Physics2D.Raycast(transform.position, transform.up + -transform.right, raysizeW, pared);
        RaycastHit2D hitDownLeft = Physics2D.Raycast(transform.position, -transform.up + -transform.right, raysizeW, pared);
        RaycastHit2D hitUpRight = Physics2D.Raycast(transform.position, transform.up + transform.right, raysizeW, pared);
        RaycastHit2D hitDownRight = Physics2D.Raycast(transform.position, -transform.up + transform.right, raysizeW, pared);




        if (hitUp.collider != null)
            sensor.AddObservation(hitUp.distance);
        else
            sensor.AddObservation(-1);

        if (hitDown.collider != null)
            sensor.AddObservation(hitDown.distance);
        else
            sensor.AddObservation(-1);

        if (hitRight.collider != null)
            sensor.AddObservation(hitRight.distance);
        else
            sensor.AddObservation(-1);

        if (hitLeft.collider != null)
            sensor.AddObservation(hitLeft.distance);
        else
            sensor.AddObservation(-1);

        if (hitUpLeft.collider != null)
            sensor.AddObservation(hitUpLeft.distance);
        else
            sensor.AddObservation(-1);

        if (hitDownLeft.collider != null)
            sensor.AddObservation(hitDownLeft.distance);
        else
            sensor.AddObservation(-1);

        if (hitUpRight.collider != null)
            sensor.AddObservation(hitUpRight.distance);
        else
            sensor.AddObservation(-1);

        if (hitDownRight.collider != null)
            sensor.AddObservation(hitDownRight.distance);
        else
            sensor.AddObservation(-1);


    }
   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Pared")
        {
            // print("Crash");
            //print("giving not a reward");
            AddReward(-5f);
            EndEpisode();
            
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Checkpoint")
        {
            collision.gameObject.GetComponent<CheckpointScript>().Touch();
            //print("giving reward");
            AddReward(.1f);
        }
    }
}
