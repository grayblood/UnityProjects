using UnityEngine;

public class TinyGunScript : MonoBehaviour
{
    public Transform gunPoint;

    public LayerMask playerLayer;

    int ammunition;
    int totalAmmunition;
    int shootCadency;
    int shootSpeed;



    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(this.transform.position, this.transform.forward, 20, ~playerLayer);
            Debug.DrawRay(gunPoint.position, gunPoint.TransformDirection(Vector3.forward) * 1000, Color.red,2f);
            if (hits != null)
            {
                foreach (RaycastHit hit in hits)
                {
                    if (hit.collider.GetComponent<Enemy_Script>() != null)
                    {
                        Debug.Log("Did Hit");
                    }
                }
            }
        }
    }
}
