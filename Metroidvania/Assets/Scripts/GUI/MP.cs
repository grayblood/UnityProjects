using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MP : MonoBehaviour
{
    [SerializeField]
    private Image bar;
    [SerializeField]
    private ScriptableFloat m_mp;

    //Scriptable object vida, enviar event al player, observer

    // Start is called before the first frame update
    void Start()
    {
        m_mp.value = 10f;
        bar.fillMethod = Image.FillMethod.Horizontal;
        bar.type = Image.Type.Filled;
    }

    // Update is called once per frame

    public void FillMP()
    {
        bar.fillAmount = m_mp.value / 500.0f;
    }
}

