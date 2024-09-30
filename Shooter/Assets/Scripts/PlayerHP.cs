using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public int hp;
    [SerializeField]
    private int hpMax;
    public TextMesh HUD_HP;

    void Awake()
    {
        hp = hpMax;
        HUD_HP.text = hp.ToString();
    }

    void Update()
    {
        if (hp <= 0)
        {
            Destroy(gameObject);
            GunInventory GI = this.GetComponent<GunInventory>();
            GI.DeadMethod();
            
        }
    }

    public void PJHit(int DMG)
    {
        hp -= DMG;
        HUD_HP.text = hp.ToString();
        print("Me ha hecho daño un botardo: " + DMG);
    }
}
