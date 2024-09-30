using UnityEngine;

public class AxeBehaviour : MonoBehaviour
{

    public float Speed;
    public Vector2 direction = Vector2.right;
    public float tamano;

    // Start is called before the first frame update
    void Start()
    {
        //objeto hace un arco(con fisicas)
    
            gameObject.GetComponent<Rigidbody2D>().AddForce(direction * Speed, ForceMode2D.Impulse);
            transform.localScale = new Vector3(tamano, tamano, tamano);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(new Vector3(0f, 0f, 1f) * 3f);
        if (gameObject.transform.position.y <= -5)
        {
            Destroy(gameObject);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            //daño
        }
    }
}
