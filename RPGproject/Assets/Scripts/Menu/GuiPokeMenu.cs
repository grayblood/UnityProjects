using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuiPokeMenu : MonoBehaviour
{
    [SerializeField]
    GameObject pokeMenu;

    public void ActivateMenu(bool status)
    {
        pokeMenu.SetActive(status);
    }
}
