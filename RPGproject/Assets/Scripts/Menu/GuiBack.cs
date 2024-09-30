using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiBack : MonoBehaviour
{
    [SerializeField]
    GameObject backMenu;

    public void ActivateMenu(bool status)
    {
        backMenu.SetActive(status);
    }
}
