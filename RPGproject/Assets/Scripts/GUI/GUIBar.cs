using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIBar : MonoBehaviour
{
    public Image bar;

    public ScriptableBar barra;

    void Start()
    {
        bar.fillMethod = Image.FillMethod.Horizontal;
        bar.type = Image.Type.Filled;
    }
    public void FillHP(){
        bar.fillAmount = (float)barra.currentValue / (float)barra.maxValue;
        print(bar.fillAmount);
    }
}
