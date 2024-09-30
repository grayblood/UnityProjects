using UnityEngine;

using UnityEngine.SceneManagement;
public enum GameState { Free, Battle, Dialog }
public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;

    GameState state;
    public static GameController Instance;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        playerController.OnEncountered += StartBattle;
        /*
        playerController.OnEnterTrainerView += (Collider2D colliderEntrenador) =>
        {
            var Entrenador = colliderEntrenador.GetComponentInParent<TrainerController>();
            if(Entrenador != null)
            {
               // StartCoroutine(Entrenador.TriggerBattle(playerController));
            }
        };
        */
        DialogManager.Instance.OnShowDialog += () =>
        {
            state = GameState.Dialog;
        };
        DialogManager.Instance.OnCloseDialog += () =>
        {

            if (state == GameState.Dialog)
                state = GameState.Free;
        };

    }
    void StartBattle()
    {
        state = GameState.Battle;
        SavingSystem.i.Save("combat");
       
        SceneManager.LoadScene("Combate", LoadSceneMode.Single);
        
    }

   
    void EndBattle(bool won)
    {
        state = GameState.Free;
        SavingSystem.i.Load("combat");
    }
    private void Update()
    {
        if (state == GameState.Free)
        {
            playerController.HandleUpdate();
        }
        else if (state == GameState.Battle)
        {
            StartBattle();
        }
        else if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }

    }
}
