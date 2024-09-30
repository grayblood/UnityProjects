using System.Collections;
using UnityEngine;

public class Bat : MonoBehaviour
{
    [SerializeField]
    private int hp;
    [SerializeField]
    private Vector3 m_spawnPos;
    [SerializeField]
    private float m_idleLimit;
    [SerializeField]
    private float m_offset;
    [SerializeField]
    private float m_distance;
    [SerializeField]
    private Vector3 m_target;
    [SerializeField]
    private int m_times;
    [SerializeField]
    private Vector3[] m_bezier;
    [SerializeField]
    private GameObject m_player;
    [SerializeField]
    private int m_i;
    [SerializeField]
    private int m_state;
    [SerializeField]
    private bool m_detect;
    [SerializeField]
    private int deathImpulse;
    [SerializeField]
    PhysicsMaterial2D material;
    [SerializeField]
    private ParticleSystem particle;
    public int m_atk;
    public float m_speed;
    public SpriteRenderer m_sr;
    public Animator m_anim;
    public Vector3 m_initialPos;
    private bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        m_sr = GetComponent<SpriteRenderer>();
        m_spawnPos = transform.position;
        m_target = new Vector3(m_spawnPos.x + 5, transform.position.y, 0f);
        m_player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("Idle");
        m_state = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (alive) ChangeState(m_state);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0, m_offset, 0), m_distance);
    }

    void ChangeState(int i)
    {
        switch (i)
        {
            case 1:
                IAController();
                break;
            case 2:
                Pursuit();
                break;
            case 3:
                Overfly();
                break;
            case 4:
                StartAttack();
                break;
            case 5:
                Attack();
                break;
        }
    }

    void IAController()
    {
        if (Vector2.Distance(m_player.transform.position, transform.position + new Vector3(0, m_offset, 0)) < m_distance) 
            m_detect = true;
        if (m_detect)
        {
            StopAllCoroutines();
            m_state = 2;
        }
    }
    
    IEnumerator Idle()
    {
        yield return new WaitForEndOfFrame();
        if (transform.position.x == m_target.x)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
            yield return new WaitForSeconds(Random.Range(2, 4));
             if (transform.position.x >= m_spawnPos.x)
                m_target.x = Random.Range(-m_idleLimit + m_spawnPos.x, m_spawnPos.x - 2);
             else
                m_target.x = Random.Range(m_spawnPos.x + 2, m_idleLimit + m_spawnPos.x);
            m_sr.flipX = !m_sr.flipX;
        }
        else GetComponent<Rigidbody2D>().MovePosition(Vector3.MoveTowards(transform.position, m_target, m_speed));
        StartCoroutine("Idle");
    }

    void Overfly()
    {
        if (m_times > 0)
        {
            if (transform.position.x == m_target.x)
            {
                if (transform.position.x > m_player.transform.position.x)
                    m_target = new Vector3(m_player.transform.position.x - 3, transform.position.y, 0);
                else
                    m_target = new Vector3(m_player.transform.position.x + 3, transform.position.y, 0);
                m_sr.flipX = !m_sr.flipX;
                m_times--;
            }
            else GetComponent<Rigidbody2D>().MovePosition(Vector3.MoveTowards(transform.position, m_target, m_speed * 1.5f));
        }
        else m_state = 4;
    }

    void Pursuit()
    {
        Vector3 target = new Vector3(m_player.transform.position.x, m_player.transform.position.y + 3, 0);
        if (transform.position != target)
            GetComponent<Rigidbody2D>().MovePosition(Vector3.MoveTowards(transform.position, target, m_speed * 1.5f));
        else
        {
            m_target = new Vector3(m_player.transform.position.x - 3, m_player.transform.position.y + 3, 0);
            m_state = 3;
        }
    }

    IEnumerator PrepareAttack()
    {
        m_state = 0;
        Vector3 end;
        Vector3 player = new Vector3(m_player.transform.position.x, m_player.transform.position.y - 2, 0f);
        if(transform.position.x > m_player.transform.position.x)
            end = new Vector3(m_player.transform.position.x - 4, transform.position.y, 0f);
        else
            end = new Vector3(m_player.transform.position.x + 4, transform.position.y, 0f);
        m_bezier = GetBezierCurve(transform.position, player, end, 5);
        m_times = Random.Range(2, 4);
        m_i = 0;
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(1f);
        m_state = 5;
    }

    void StartAttack() => StartCoroutine("PrepareAttack");

    void Attack()
    {
        if (m_i < m_bezier.Length)
        {
            if (transform.position != m_bezier[m_i])
                GetComponent<Rigidbody2D>().MovePosition(Vector3.MoveTowards(transform.position, m_bezier[m_i], m_speed * 2));
            else m_i++;
        }
        else m_state = 3;
    }

    Vector3 CalculatePoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;

        return p;
    }

    Vector3[] GetBezierCurve(Vector3 startPoint, Vector3 controlPoint, Vector3 endPoint, int segmentNum)
    {
        Vector3[] path = new Vector3[segmentNum];
        for (int i = 1; i <= segmentNum; i++)
        {
            float t = i / (float)segmentNum;
            Vector3 pixel = CalculatePoint(t, startPoint, controlPoint, endPoint);
            path[i - 1] = pixel;
        }
        return path;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player_Hitbox")
        {
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

        if (hp <= 0)
        {
            alive = false;
            if (x < this.gameObject.transform.position.x)
            {
                this.gameObject.GetComponent<Rigidbody2D>().AddForce((EnemyPosition - PlayerPosition).normalized * deathImpulse, ForceMode2D.Impulse);
            }
            if (x > this.gameObject.transform.position.x)
            {
                this.gameObject.GetComponent<Rigidbody2D>().AddForce(PlayerPosition - EnemyPosition.normalized * deathImpulse, ForceMode2D.Impulse);

            }
            this.gameObject.GetComponent<Rigidbody2D>().sharedMaterial = material;
            this.gameObject.layer = 10;

            Destroy(this.gameObject, 1f);

        }

    }
}
