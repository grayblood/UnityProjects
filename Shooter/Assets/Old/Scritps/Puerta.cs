using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    Animator anim;
    void Awake()
    {
        anim = this.GetComponent<Animator>();
    }
    public void MoverPuerta(bool Active)
    {
        anim.SetBool("OutIdlePuerta", true);
        if (Active)
        {
            print("Abrir ABOBOLE");
            anim.SetBool("ActivePuerta", true);
        }
        else if (!Active)
        {
            print("Cerrar ABOBOLE");
            anim.SetBool("ActivePuerta", false);
        }
    }
}

