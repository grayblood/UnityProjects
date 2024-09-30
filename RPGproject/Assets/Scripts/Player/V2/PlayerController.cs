using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour, ISavable
{

    public event Action OnEncountered;
    public event Action<Collider2D> OnEnterTrainerView;
    private Vector2 input;
    bool movement;


    private Character character;
    private void Awake()
    {
        movement = true;
        character = GetComponent<Character>();
        

    }


    public void HandleUpdate()
    {
        if (movement)
        {
            if (!character.IsMoving)
            {
                input.x = Input.GetAxisRaw("Horizontal");
                input.y = Input.GetAxisRaw("Vertical");


                //remove diagonal movement
                if (input.x != 0) input.y = 0;


                if (input != Vector2.zero)
                {

                    StartCoroutine(character.Move(input, OnMoveOver));

                }
                if (Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    GetComponent<PlayerEvent>().OnMenu();
                }
            }
            character.HandleUpdate();

            if (Input.GetKeyDown(KeyCode.Z))
            {
                Debug.Log("333?");
                Interact();

            }

        }



    }

    void Interact()
    {


        var facingDir = new Vector3(character.Animator.GetFloat("moveX"), character.Animator.GetFloat("moveY"));
        var interactPos = transform.position + facingDir;
        // Debug.DrawLine(transform.position, interactPos, Color.black, 0.5f);
        var collider = Physics2D.OverlapCircle(interactPos, 0.3f, GameLayers.i.InteractableLayer);
        if (collider != null)
        {
            collider.GetComponent<Interactable>()?.interact();
        }
    }
    private void OnMoveOver()
    {
        CheckForEncounters();
        CheckIfInTrainersView();
    }



    private void CheckIfInTrainersView()
    {
        var collider = Physics2D.OverlapCircle(transform.position, 0.2f, GameLayers.i.FovLayer);
        if (collider != null)
        {
            SavingSystem.i.Save("combat");

            SceneManager.LoadScene("Combate", LoadSceneMode.Single);
        }
    }
    private void CheckForEncounters()
    {
        if (Physics2D.OverlapCircle(transform.position, 0.2f, GameLayers.i.GrassLayer) != null)
        {
            if (Random.Range(1, 101) <= 10)
            {
                character.Animator.SetBool("isMoving", false);

                OnEncountered();
                Debug.Log("Encuentro salvaje");
            }
        }
    }

    public void OnPause()
    {

        movement = false;
    }

    public void OnResume()
    {
        movement = true;
    }

    public object CaptureState()
    {
        float[] position = new float[] { transform.position.x, transform.position.y };
        return position;
    }

    public void RestoreState(object state)
    {
        var position = (float[])state;
        transform.position = new Vector3(position[0], position[1]);

    }
}
