using UnityEngine;

public class Character : MonoBehaviour
{
    public int vidaMax = 10;
    int currentVida;

    private void Awake()
    {
        currentVida = vidaMax;
    }

    virtual public void TakeDamage(int dmg)
    {
        currentVida -= dmg;
        if (currentVida <= 0)
        {
            death();
        }
    }
    virtual public void death()
    {
        Destroy(gameObject);
    }
}
