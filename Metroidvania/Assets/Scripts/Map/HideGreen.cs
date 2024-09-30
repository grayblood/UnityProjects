using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideGreen : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            this.gameObject.SetActive(false);
        }
    }

}
