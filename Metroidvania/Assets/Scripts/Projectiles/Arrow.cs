using UnityEngine;

public class Arrow : Projectile
{
    public float Speed;



    public override void Init(Transform target, float tamano, float tiempo)
    {
        Vector2 m_direction = target.position - transform.position;
        //angulos negativos

        float angle = Vector2.Angle(m_direction, Vector2.right);
        if (m_direction.y < 0)
        {
            angle *= -1;
        }
        transform.localEulerAngles = new Vector3(0, 0, angle);

        GetComponent<Rigidbody2D>().velocity = transform.right * Speed;
        
        transform.localScale = new Vector3(tamano, tamano, tamano);
        Destroy(gameObject, tiempo);
    }


    // Update is called once per frame
    // f(x) = x. + Vx * t
   
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Obstaculo")
        {

            Destroy(gameObject);
        }

       
    }

 
}
