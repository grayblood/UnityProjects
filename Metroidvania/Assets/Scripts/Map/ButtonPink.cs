using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPink : MonoBehaviour
{
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private GameObject fill;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player_Hitbox") {
            this.GetComponent<Rigidbody2D>().transform.position = new Vector3(this.GetComponent<Rigidbody2D>().transform.position.x+0.45f, this.GetComponent<Rigidbody2D>().transform.position.y, this.GetComponent<Rigidbody2D>().transform.position.z);
            door.SetActive(false);
            fill.SetActive(false);
            this.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
