using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPokemon : MonoBehaviour
{
     public BasePokemon pokemon;

    public Sprite sprite;
    public string name;
    public int m_level;
    public int m_ps;
    public int m_psMax;
    public int m_pp;
    public int m_ppMax;
    public int m_exp;
    public int m_atk;
    public int m_def;
    public float m_speed;
    public bool m_owner;
    public bool m_ally;
    public Tipo tipo;
    //[SerializeField]
    //ItemsScriptable item;
    //[SerializeField]
    private Attack[] m_attacks;

    //ITEM
    //-------------------------------------
    private Attack MainAttack;
    private Attack m_Slash;
    private Attack m_Protect;
    float Cooldown1 = 0f;
    float Cooldown2 = 0f;
    float Cooldown3 = 0f;
    float CooldownMp = 0f;
    float CooldownMove = 0f;
    int lastMove = 0;


    private void Start()
    {
        sprite = pokemon.sprite;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        name = pokemon.name;
        m_level = pokemon.m_level;
        m_ps = pokemon.m_ps;
        m_psMax = pokemon.m_psMax;
        m_pp = pokemon.m_pp;
        m_ppMax = pokemon.m_ppMax;
        m_exp = pokemon.m_exp;
        m_atk = pokemon.m_atk;
        m_def = pokemon.m_def;
        m_speed = pokemon.m_speed;
        m_owner = pokemon.m_owner;
        m_ally = pokemon.m_ally;
        tipo = pokemon.tipo;
        m_attacks = pokemon.m_attacks;
        MainAttack = pokemon.m_attacks[0];
        m_Slash = pokemon.m_attacks[1];
        m_Protect = pokemon.m_attacks[2];
        CooldownMp = 1f;
        CooldownMove = 0.5f;
    }

    private void Update()
    {
        Cooldown1 -= Time.deltaTime;
        Cooldown2 -= Time.deltaTime;
        Cooldown3 -= Time.deltaTime;
        CooldownMp -= Time.deltaTime;
        CooldownMove -= Time.deltaTime;
        //Move();
        Attack();
        MpRegen();
    }

    public void Attack()
    {
            if (Cooldown1 <= 0.0f)
            {
                int r = Random.Range(1,6);
                if(r == 1 || r == 2){
                    Attack1();
                }
                if(r == 3 || r == 4){
                    Attack2();
                }
                if(r == 5){
                    Attack3();
                }
                Cooldown1 = 4f;
            }
        
    }
    public void LevelUp()
    {
        m_level++;
        m_psMax++;
        m_ps = m_psMax++;
        m_ppMax++;
        m_pp = m_ppMax++;
        m_exp = 0;
        m_atk++;
    }
    public void RecieveDamage(int dmg)
    {
        this.m_ps -= dmg;
        if(this.m_ps <= 0){
            Defeat();
        }
    }
    public void Defeat()
    {
        Debug.Log(this.gameObject + "esta debilitado!");
        Destroy(this.gameObject);
    }

    public void Move()
    {
        if(CooldownMove <= 0.0f){

                int r = Random.Range(1,5);
                if(r == lastMove){
                    r++;
                }
                if(r == 1){
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, m_speed);
                }
                if(r == 2){
                    GetComponent<Rigidbody2D>().velocity = new Vector2(-m_speed, 0);
                }
                if(r == 3){
                    GetComponent<Rigidbody2D>().velocity = new Vector2(0, -m_speed);
                }
                if(r == 4){
                    GetComponent<Rigidbody2D>().velocity = new Vector2(m_speed, 0);
                }



            CooldownMove = 0.5f;
        }
        /*AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
        if (Input.GetKey(KeyCode.UpArrow))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, m_speed);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(-m_speed, 0);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, -m_speed);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(m_speed, 0);
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
        */
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Attack") {
            RecieveDamage(1);
            Debug.Log(this.gameObject+"ha sido golpeado por un enemigo! Vida restant: "+ m_ps);

            
        }
    }

    public void MpRegen(){
        if(m_pp < m_ppMax){
            if(CooldownMp <= 0.0f){
                m_pp+= 2;
                CooldownMp = 1f;
            }
        }
    }


    public void Attack1()
    {
        if(m_pp >= MainAttack.m_pp){
        if(MainAttack.m_type.Equals(Tipo.Fuego)){
            StartCoroutine(Ember());
        }else if(MainAttack.m_type.Equals(Tipo.Agua)){
            StartCoroutine(WaterGun());
        }else if(MainAttack.m_type.Equals(Tipo.Planta)){
            StartCoroutine(RazorLeaf());
        }
        }
    }
    public void Attack2()
    {
        if(m_pp >= m_Slash.m_pp){
        StartCoroutine(Slash());
        }
    }
    public void Attack3()
    {
        if(m_pp >= m_Protect.m_pp){
        StartCoroutine(Protect());
        }
    }


    IEnumerator Ember() {
        m_pp -= MainAttack.m_pp;
        GameObject clone = Instantiate(MainAttack.m_sprite);
        clone.tag = "EnemyAttack";
        clone.transform.position = new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z - 5);
        clone.GetComponent<Rigidbody2D>().velocity = (new Vector3(-10, 0, 0));
        clone.transform.Rotate(0.0f,0.0f,180.0f,Space.Self);
        yield return new WaitForSeconds(2);

        Destroy(clone);

    }

    IEnumerator WaterGun()
    {
        m_pp -= MainAttack.m_pp;
        GameObject clone = Instantiate(MainAttack.m_sprite);
        clone.tag = "EnemyAttack";
        clone.transform.position = new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z - 5);
        clone.GetComponent<Rigidbody2D>().velocity = (new Vector3(-6, 0, 0));
        clone.transform.Rotate(0.0f,0.0f,180.0f,Space.Self);
        yield return new WaitForSeconds(3);

        Destroy(clone);

    }

    IEnumerator RazorLeaf()
    {
        m_pp -= MainAttack.m_pp;
        GameObject clone = Instantiate(MainAttack.m_sprite);
        clone.tag = "EnemyAttack";
        clone.transform.position = new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z - 5);
        clone.GetComponent<Rigidbody2D>().velocity = (new Vector3(-5, 1, 0));
        clone.transform.Rotate(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z - 5);
        yield return new WaitForSeconds(2);
        clone.GetComponent<Rigidbody2D>().velocity = (new Vector3(5, 1, 0));
        yield return new WaitForSeconds(2);
        Destroy(clone);

    }

    IEnumerator Slash() {
        m_pp -= m_Slash.m_pp;
        GameObject clone = Instantiate(m_Slash.m_sprite);
        clone.tag = "EnemyAttack";
        clone.transform.position = new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z - 5);
        clone.GetComponent<Rigidbody2D>().velocity = (new Vector3(-20, 0, 0));
        clone.transform.Rotate(0.0f,0.0f,180.0f,Space.Self);
        yield return new WaitForSeconds(0.8f); 
            
        Destroy(clone);

    }

    IEnumerator Protect() {
        m_pp -= m_Protect.m_pp;
        GameObject clone = Instantiate(m_Protect.m_sprite);
        clone.transform.position = new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z - 5);
        yield return new WaitForSeconds(0.3f);

        Destroy(clone);
    }



}
