using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    Animator anim;

    private void Awake()
    {
        anim = this.GetComponent<Animator>();
    }
    // Update is called once per frame
    public void MoverPalanca (bool Active)
    {
        anim.SetBool("OutIdle", true);
        if (Active)
        {
            print("Activar ABOBOLE");
            anim.SetBool("Activated", true);
        }
        else if (!Active)
        {
            print("Desactivar ABOBOLE");
            anim.SetBool("Activated", false);
        }
    }
}
