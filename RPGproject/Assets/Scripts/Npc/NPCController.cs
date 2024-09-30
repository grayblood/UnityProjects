using UnityEngine;

public class NPCController : MonoBehaviour, Interactable
{
    [SerializeField] Dialogo dialogo;
    public void interact()
    {

        StartCoroutine(DialogManager.Instance.ShowDialog(dialogo, () =>
        {
            //para detener el npc

        }));

    }
}
