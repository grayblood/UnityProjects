using UnityEngine;

public class PlayerEvent : MonoBehaviour
{

    [SerializeField]
    private GameEvent m_remove;

    private void Awake()
    {
    }

    public void OnTouch()
    {

        m_remove.Raise();

    }
}
