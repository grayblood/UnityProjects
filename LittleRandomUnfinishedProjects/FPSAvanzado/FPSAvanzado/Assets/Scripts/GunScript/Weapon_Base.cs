using UnityEngine;

public class Weapon_Base : MonoBehaviour
{
    public int maxClipAmmo = 0;
    public int currentClipAmmo = 0;
    public int maxHoldAmmo = 0;
    public int currentHoldAmmo = 0;

    virtual public void Shoot()
    {

    }

    virtual public void Reload()
    {

    }


}
