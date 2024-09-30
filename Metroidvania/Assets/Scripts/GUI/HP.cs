using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    [SerializeField]
    private Image bar;
    [SerializeField]
    private ScriptableFloat m_hp;

    //Scriptable object vida, enviar event al player, observer

    // Start is called before the first frame update
    void Start()
    {
        m_hp.value = 10f;
        bar.fillMethod = Image.FillMethod.Horizontal;
        bar.type = Image.Type.Filled;
    }

    // Update is called once per frame

    public void FillHP()
    {
        bar.fillAmount = m_hp.value / 10.0f;
    }
}
