using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiMenuRight : MonoBehaviour
{
    [SerializeField]
    GameObject RightMenu;

    public void ActivateMenu(bool status)
    {
        RightMenu.SetActive(status);
    }
}
