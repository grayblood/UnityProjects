using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] int speedLetter;

    public event Action OnShowDialog;
    public event Action OnCloseDialog;

    public static DialogManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }



    Dialogo dialogo;
    Action onDialogFinished;
    int currentLine = 0;
    bool escribiendo;

    public IEnumerator ShowDialog(Dialogo dialogo, Action onFinished = null)
    {
        yield return new WaitForEndOfFrame();

        OnShowDialog?.Invoke();
        this.dialogo = dialogo;
        onDialogFinished = onFinished;

        dialogBox.SetActive(true);
        StartCoroutine(EscribirDialogo(dialogo.Lines[0]));
    }

    public void HandleUpdate()
    {
        Debug.Log("23?");

        if (Input.GetKeyDown(KeyCode.Z) && !escribiendo)
        {
            Debug.Log("funciona?");
            ++currentLine;
            if(currentLine < dialogo.Lines.Count)
            {
                StartCoroutine(EscribirDialogo(dialogo.Lines[currentLine]));
            }
            else
            {
                currentLine = 0;
                dialogBox.SetActive(false);
                onDialogFinished?.Invoke();
                OnCloseDialog?.Invoke();
            }
        }
    }
    public IEnumerator EscribirDialogo(string line)
    {
        escribiendo = true;
        dialogText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / speedLetter);
        }
        escribiendo = false;
    }
}
