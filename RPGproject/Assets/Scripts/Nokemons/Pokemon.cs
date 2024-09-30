using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Pokemon : MonoBehaviour
{
    [SerializeField]
    private bool hasIA;
    [SerializeField]
    private BasePokemon pokemon;
    [SerializeField]
    private ScriptableBar HP;
    [SerializeField]
    private ScriptableBar PP;
    [SerializeField]
    private BattleManager bm;

    [SerializeField]
    private GameEvent OnHP;
    [SerializeField]
    private GameEvent OnPP;

    public Sprite sprite;
    public string name;
    [SerializeField] public int m_level;
    public int m_ps {get;set;}
    public int m_psMax { get; set; }


    public int m_pp;
    public int m_ppMax;
    public int m_exp;
    public int m_atk;
    public int m_def;
    public float m_speed;
    public Tipo tipo;
    //[SerializeField]
    //ItemsScriptable item;
    //[SerializeField]
    private Attack[] m_attacks;
    public BasePokemon[] m_team;
    int pokemonActual;

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


    public BasePokemon Base
    {
        get
        {
            return pokemon;
        }
    }

    public int m_Level
    {
        get
        {
            return m_level;
        }
    }

    private void Start()
    {
        CooldownMp = 1f;
        CooldownMove = 0.5f;
        if (!hasIA) {
            m_team = bm.PlayerTeam;
        }
        else {
            m_team = bm.EnemyTeam;
        }
        pokemonActual = 0;
        SetPokemon(m_team[pokemonActual]);

    }

    private void Update()
    {
        Cooldown1 -= Time.deltaTime;
        Cooldown2 -= Time.deltaTime;
        Cooldown3 -= Time.deltaTime;
        CooldownMp -= Time.deltaTime;
        Move();
        Attack();
        MpRegen();
    }

    public void SetPokemon(BasePokemon pokemonActivo) {
        print(pokemonActivo);
        sprite = pokemonActivo.sprite;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
        name = pokemonActivo.name;
        m_level = pokemonActivo.m_level;
        m_ps = pokemonActivo.m_ps;
        m_psMax = pokemonActivo.m_psMax;
        m_pp = pokemonActivo.m_pp;
        m_ppMax = pokemonActivo.m_ppMax;
        m_exp = pokemonActivo.m_exp;
        m_atk = pokemonActivo.m_atk;
        m_def = pokemonActivo.m_def;
        m_speed = pokemonActivo.m_speed;
        tipo = pokemonActivo.tipo;
        m_attacks = pokemonActivo.m_attacks;
        MainAttack = pokemonActivo.m_attacks[0];
        m_Slash = pokemonActivo.m_attacks[1];
        m_Protect = pokemonActivo.m_attacks[2];
        HP.currentValue = pokemonActivo.m_ps;
        PP.currentValue = pokemonActivo.m_pp;
        HP.maxValue = pokemonActivo.m_psMax;
        PP.maxValue = pokemonActivo.m_ppMax;
    }

    public void Attack()
    {
        if (!hasIA)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                if (Cooldown1 <= 0.0f)
                {
                    Attack1();
                    Cooldown1 = MainAttack.m_cooldown;
                }
            }
            else if (Input.GetKey(KeyCode.X))
            {
                if (Cooldown2 <= 0.0f)
                {
                    Attack2();
                    Cooldown2 = m_Slash.m_cooldown;
                }
            }
            else if (Input.GetKey(KeyCode.C))
            {
                if (Cooldown3 <= 0.0f)
                {
                    Attack3();
                    Cooldown3 = m_Protect.m_cooldown;
                }
            }
        }
        else
        {
            if (Cooldown1 <= 0.0f)
            {
                int r = Random.Range(1, 6);
                if (r == 1 || r == 2)
                {
                    Attack1();
                }
                if (r == 3 || r == 4)
                {
                    Attack2();
                }
                if (r == 5)
                {
                    Attack3();
                }
                Cooldown1 = 4f;
            }
        }
    }

    private void SetHP(int value)
    {
        this.m_ps = value;
        HP.currentValue = value;
        OnHP.Raise(); 
    }

    private void SetPP(int value)
    {
        this.m_pp = value;
        PP.currentValue = value;
        OnPP.Raise();
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

    public int DamageCalculation() {
        int pokemonDmg = this.MainAttack.m_damage - this.m_def / 2;
        return pokemonDmg;
    }
    public void RecieveDamage(int dmg)
    {
        SetHP(m_ps - dmg);
        if (this.m_ps <= 0)
        {
            Defeat();
        }
    }
    public void Defeat()
    {
        Debug.Log(this.gameObject + "esta debilitado!");
        NextPokemon(this.m_team);
        
    }

    
    public void NextPokemon(BasePokemon[] pkmn) {
        if (pokemonActual <= 4)
        {
            pokemonActual++;
            SetPokemon(m_team[pokemonActual]);
            OnHP.Raise();
            OnPP.Raise();
        }
        else {
            LevelUp();
            SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
        }
    }
    public void Move()
    {
        if (!hasIA)
        {
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
        }
        else
        {
           // GetComponent<Rigidbody2D>().velocity = new Vector2(0, m_speed);

            //Movimiento por posicion de x e y.
            //if (this.transform.position.y >= 3) {
          //      GetComponent<Rigidbody2D>().velocity = new Vector2(0, -m_speed);
           // }
           // if (this.transform.position.y <= -3) {
                //GetComponent<Rigidbody2D>().velocity = new Vector2(0, m_speed);
           // }

          /*  if (this.transform.position.x >= 7)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-m_speed, 0);
            }
            if (this.transform.position.x <= 2)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(m_speed,0);
            }
            */
            
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasIA) {
            if (collision.tag == "EnemyAttack")
            {
                RecieveDamage(DamageCalculation());
                Debug.Log(this.gameObject + "ha sido golpeado por un enemigo! Vida restant: " + m_ps);


            }
        }
        else {
            if (collision.tag == "Attack")
            {
                RecieveDamage(DamageCalculation());
                Debug.Log(this.gameObject + "enemigo golpeado! Vida restant: " + m_ps);


            }
        }
    }

    public void MpRegen()
    {
        if (m_pp < m_ppMax)
        {
            if (CooldownMp <= 0.0f)
            {
                SetPP(m_pp + 2);
                CooldownMp = 1f;
            }
        }
    }


    public void Attack1()
    {
        if (m_pp >= MainAttack.m_pp)
        {
            if (MainAttack.m_type.Equals(Tipo.Fuego))
            {
                StartCoroutine(Ember());
            }
            else if (MainAttack.m_type.Equals(Tipo.Agua))
            {
                StartCoroutine(WaterGun());
            }
            else if (MainAttack.m_type.Equals(Tipo.Planta))
            {
                StartCoroutine(RazorLeaf());
            }
        }
    }
    public void Attack2()
    {
        if (m_pp >= m_Slash.m_pp)
        {
            StartCoroutine(Slash());
        }
    }
    public void Attack3()
    {
        if (m_pp >= m_Protect.m_pp)
        {
            StartCoroutine(Protect());
        }
    }


    IEnumerator Ember()
    {
        if (!hasIA)
        {
            SetPP(m_pp - MainAttack.m_pp);
            GameObject clone = Instantiate(MainAttack.m_sprite);
            clone.transform.position = new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z - 5);
            clone.GetComponent<Rigidbody2D>().velocity = (new Vector3(10, 0, 0));
            yield return new WaitForSeconds(2);

            Destroy(clone);
        }
        else {
            m_pp -= MainAttack.m_pp;
            GameObject clone = Instantiate(MainAttack.m_sprite);
            clone.tag = "EnemyAttack";
            clone.transform.position = new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z - 5);
            clone.GetComponent<Rigidbody2D>().velocity = (new Vector3(-10, 0, 0));
            clone.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
            yield return new WaitForSeconds(2);

            Destroy(clone);
        }
    }

    IEnumerator WaterGun()
    {
        if (!hasIA)
        {
            SetPP(m_pp - MainAttack.m_pp);
            GameObject clone = Instantiate(MainAttack.m_sprite);
            clone.transform.position = new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z - 5);
            clone.GetComponent<Rigidbody2D>().velocity = (new Vector3(6, 0, 0));
            yield return new WaitForSeconds(3);

            Destroy(clone);
        }
        else {
            m_pp -= MainAttack.m_pp;
            GameObject clone = Instantiate(MainAttack.m_sprite);
            clone.tag = "EnemyAttack";
            clone.transform.position = new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z - 5);
            clone.GetComponent<Rigidbody2D>().velocity = (new Vector3(-6, 0, 0));
            clone.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
            yield return new WaitForSeconds(3);

            Destroy(clone);
        }
    }

    IEnumerator RazorLeaf()
    {
        if (!hasIA)
        {
            SetPP(m_pp - MainAttack.m_pp);
            GameObject clone = Instantiate(MainAttack.m_sprite);
            clone.transform.position = new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z - 5);
            clone.GetComponent<Rigidbody2D>().velocity = (new Vector3(5, 0, 0));
            clone.transform.Rotate(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z - 5);
            yield return new WaitForSeconds(2);
            clone.GetComponent<Rigidbody2D>().velocity = (new Vector3(-5, 0, 0));
            yield return new WaitForSeconds(1);
            Destroy(clone);
        }
        else {
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
    }

    IEnumerator Slash()
    {
        if (!hasIA)
        {
            SetPP(m_pp - m_Slash.m_pp);
            GameObject clone = Instantiate(m_Slash.m_sprite);
            clone.transform.position = new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z - 5);
            clone.GetComponent<Rigidbody2D>().velocity = (new Vector3(20, 0, 0));
            yield return new WaitForSeconds(0.8f);

            Destroy(clone);
        }
        else {
            m_pp -= m_Slash.m_pp;
            GameObject clone = Instantiate(m_Slash.m_sprite);
            clone.tag = "EnemyAttack";
            clone.transform.position = new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z - 5);
            clone.GetComponent<Rigidbody2D>().velocity = (new Vector3(-20, 0, 0));
            clone.transform.Rotate(0.0f, 0.0f, 180.0f, Space.Self);
            yield return new WaitForSeconds(0.8f);

            Destroy(clone);
        }
    }

    IEnumerator Protect()
    {
        if (!hasIA) {
            SetPP(m_pp - m_Protect.m_pp);
            GameObject clone = Instantiate(m_Protect.m_sprite);
            clone.transform.position = new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z - 5);
            yield return new WaitForSeconds(0.3f);

            Destroy(clone);
        }
        else {
            m_pp -= m_Protect.m_pp;
            GameObject clone = Instantiate(m_Protect.m_sprite);
            clone.transform.position = new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z - 5);
            yield return new WaitForSeconds(0.3f);

            Destroy(clone);
        }
    }

}
