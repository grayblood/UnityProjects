using UnityEngine;

public class Enemy_Move : MonoBehaviour
{
    [SerializeField]
    private int hp;
    [SerializeField]
    private int deathImpulse;
    [SerializeField]
    PhysicsMaterial2D material;
    private Vector2 inicialpos;
    public float Speed;
    public bool left;
    public float distanciamaxima;
        [SerializeField]
    private ParticleSystem particle;

    private bool alive = true;
    void Start() //awake
    {
        inicialpos = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(alive){
        if (left)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(-Speed, GetComponent<Rigidbody2D>().velocity.y, 0);
        }
        if (!left)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(Speed, GetComponent<Rigidbody2D>().velocity.y, 0);
        }


        float totaldistneg = inicialpos.x - distanciamaxima;
        float totaldistpos = inicialpos.x + distanciamaxima;
        if (totaldistneg >= transform.position.x)
        {

            left = false;

        }
        if (totaldistpos <= transform.position.x)
        {
            left = true;
        }
        }
    }

     public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player_Hitbox") {
         
            RecieveDamage(1, collision.gameObject.transform.position.x, collision.gameObject.transform.position);
        }
    }

    public void RecieveDamage(int dmg, float x, Vector3 position)
    {

        Vector3 PlayerPosition = position;
        Vector3 EnemyPosition = this.gameObject.transform.position;

        particle.transform.position = EnemyPosition;
        particle.Play();

        hp -= dmg;

        if (hp <= 0) {
            alive = false;
            if (x < this.gameObject.transform.position.x) {
                this.gameObject.GetComponent<Rigidbody2D>().AddForce((EnemyPosition - PlayerPosition).normalized * deathImpulse, ForceMode2D.Impulse);
            }
            if (x > this.gameObject.transform.position.x) {
                this.gameObject.GetComponent<Rigidbody2D>().AddForce(PlayerPosition - EnemyPosition.normalized * deathImpulse, ForceMode2D.Impulse);

            }
            this.gameObject.GetComponent<Rigidbody2D>().sharedMaterial = material;
            this.gameObject.layer = 10;

            Destroy(this.gameObject,1f);
            
        }

    }
}
