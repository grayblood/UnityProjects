using UnityEngine;


namespace Mirror.Examples.car
{
    public class CarScript2 : NetworkBehaviour
    {
        //Rigidbody
        Rigidbody2D rb;
        PlayerEvent rem;

        //variables globales
        Vector2 startPos;
        Quaternion rotationPosition;

        //var movement
        public float maxSpeed;
        public float acceleration;
        private bool deteSand = false;
        public float steering;
        float direction;
        float tiempo = 5f;
        //velocidad
        public float currentSpeed;
        private bool spill;

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            startPos = transform.localPosition;
            rem = GetComponent<PlayerEvent>();
            rotationPosition = transform.localRotation;

        }

        public void ResetGame()
        {
            transform.localPosition = startPos;
            transform.localRotation = rotationPosition;

            direction = 0;
            GetComponent<PlayerEvent>().OnTouch();

        }



        void FixedUpdate()
        {
            if (isLocalPlayer)
                HandleMovement();

        }

        [ClientCallback]
        private void HandleMovement()
        {// Get input
            float h = -Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            if (!deteSand)
            {
                rb.velocity += rb.GetRelativeVector(Vector2.up) * acceleration * v * Time.deltaTime;
            }
            else if (deteSand)
            {
                rb.velocity += rb.GetRelativeVector(Vector2.up) * acceleration / 2 * v * Time.deltaTime;
            }
            if (spill)
            {
                tiempo -= Time.deltaTime;
                rb.angularDrag = 2;
                if (tiempo <= 0)
                {
                    spill = false;
                    rb.drag = 5;
                    tiempo = 5f;
                }
            }
            direction = Vector2.Dot(rb.velocity, rb.GetRelativeVector(Vector2.up));
            if (direction >= 0.0f)
            {
                rb.rotation += steering * h * Time.deltaTime * (rb.velocity.magnitude / maxSpeed);
            }
            else
            {
                rb.rotation -= steering * h * Time.deltaTime * (rb.velocity.magnitude / maxSpeed);
            }


            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }

            /*
            // Calculate speed from input and acceleration (transform.up is forward)
            speed = transform.up * (v * acceleration);
            rb.AddForce(speed, ForceMode2D.Force);

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
            //Debug.DrawLine(rb.position, rb.GetRelativePoint(relativeForce), Color.green);
            rb.AddForce(rb.GetRelativeVector(relativeForce) * Time.deltaTime, ForceMode2D.Force);

            // Force max speed limit
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }

            */
            currentSpeed = rb.velocity.magnitude;

        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Sand")
            {
                deteSand = true;

            }
            if (collision.gameObject.tag == "OilSpill")
            {

                spill = true;
                rem.OnTouch();
                NetworkServer.Destroy(collision.gameObject);

            }
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Sand")
            {
                deteSand = true;

            }

        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Sand")
            {
                deteSand = false;

            }

        }
    }
}
