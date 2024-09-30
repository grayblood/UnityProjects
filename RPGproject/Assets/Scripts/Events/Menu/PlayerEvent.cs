using UnityEngine;

public class PlayerEvent : MonoBehaviour
{
    [SerializeField]
    private ScriptableBool m_Menu;
    [SerializeField]
    private GameEvent m_onmenu;

    private void Awake()
    {
        m_Menu.OpenMenu = false;
    }

    public void OnMenu()
    {

        m_Menu.OpenMenu = true;
        m_onmenu.Raise();

    }
}
