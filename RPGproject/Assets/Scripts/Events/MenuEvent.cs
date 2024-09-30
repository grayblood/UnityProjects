using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuEvent : MonoBehaviour
{
    [SerializeField]
    private ScriptableBool m_Menu;
    [SerializeField]
    private GameEvent m_onBackpack;

    private void Awake()
    {
        m_Menu.OpenMenu = false;
    }

    public void OnMenu()
    {

        m_Menu.OpenMenu = true;
        m_onBackpack.Raise();

    }

}
