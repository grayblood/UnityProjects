using UnityEngine;

public class Axe : Projectile
{
    public int Speed;
    private Vector2 direction = Vector2.right;

    // f(x) = x. + Vx * t
    //f(y) = y. + Vy * t - ((9'8 * t^2) /2)
    // Start is called before the first frame update
    public override void Init(Transform target, float tamano, float tiempo)
    {
        float m_directionx = target.position.x - transform.position.x;
        float m_directiony = target.position.y - transform.position.y + ((4 * 2) / 2);


        Vector2 m_direction = new Vector2(m_directionx, m_directiony);


        gameObject.GetComponent<Rigidbody2D>().AddForce(m_direction, ForceMode2D.Impulse);

        transform.localScale = new Vector3(tamano, tamano, tamano);
        Destroy(gameObject, tiempo);
    }


    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(new Vector3(0f, 0f, 1f) * 3f);
        if (gameObject.transform.position.y <= -25)
        {
            Destroy(gameObject);
        }
    }



}
