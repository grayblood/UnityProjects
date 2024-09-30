using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public static EnemyController Instance;
    private float vidaMax = 50000;
    public float vida;
    public float coliborde = .5f;
    public bool invencible = false;

    public Transform Spawner;

    public Image circleBar;
    public Image extraBar;
    public float circlePercent = 0.3f;
    private const float circleFillAmount = 0.75f;
    // Start is called before the first frame update

    private void Awake()
    {
        vida += vidaMax;
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        CircleFill();
        ExtraFill();
    }

    public void damage()
    {
        vida -= 25;
        if (vida <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void CircleFill()
    {
        //Funcionalidad de la zona circular de el hp
        float healthPercent = vida / vidaMax;
        float circleFill = healthPercent / circlePercent;
        circleFill *= circleFillAmount;
        circleFill = Mathf.Clamp(circleFill, 0, circleFillAmount);
        circleBar.fillAmount = circleFill;
        //print("Hay que llenar: " + circleFill);
    }

    void ExtraFill()
    {
        //Funcionalidad de la parte recta del hp
        float circleAmount = circlePercent * vidaMax;
        float extraHealth = vida - circleAmount;
        float extraTotalHealth = vidaMax - circleAmount;

        float extraFill = extraHealth / extraTotalHealth;
        extraFill = Mathf.Clamp(extraFill, 0, 1);
        extraBar.fillAmount = extraFill;
    }

}
