using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public float Speed;
    public Vector2 direction = Vector2.right;
    private Vector2 inicialpos;
    public float distanciamaxima;
    public float tamano;
    void Start()
    {
        //angulos negativos
        float angle = Vector2.Angle(direction, Vector2.right);
        if (direction.y < 0)
        {
            angle *= -1;
        }
        transform.localEulerAngles = new Vector3(0, 0, angle);

        GetComponent<Rigidbody2D>().velocity = transform.right * Speed;
        inicialpos = new Vector2(transform.position.x, transform.position.y);
        transform.localScale = new Vector3(tamano, tamano, tamano);



    }

    // Update is called once per frame
    void Update()
    {

        //destruir

        if (direction.x < 0)
        {

            float totaldist = inicialpos.x - distanciamaxima;
            if (totaldist >= transform.position.x)
            {

                Destroy(gameObject);
            }
        }
        else if (direction.x > 0)
        {

            float totaldist = inicialpos.x + distanciamaxima;
            if (totaldist <= transform.position.x)
            {


                Destroy(gameObject);
            }
        }
        if (direction.y < 0)
        {

            float totaldist = inicialpos.y - distanciamaxima;
            if (totaldist >= transform.position.y)
            {

                Destroy(gameObject);
            }
        }
        else if (direction.y > 0)
        {

            float totaldist = inicialpos.y + distanciamaxima;
            if (totaldist <= transform.position.y)
            {


                Destroy(gameObject);
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Obstaculo")
        {
           
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            print("Daño");
            //daño
        }

    }


}
