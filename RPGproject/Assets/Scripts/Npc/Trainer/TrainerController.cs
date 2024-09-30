using System.Collections;
using UnityEngine;

public class TrainerController : MonoBehaviour
{

    [SerializeField] Dialogo dialog;


        Character character;
        private void Awake()
        {
            character = GetComponent<Character>();
        }

        public IEnumerator TriggerBattle(PlayerController Jugador)
        {
            yield return new WaitForSeconds(0.5f);
            var diferencia = Jugador.transform.position - transform.position;
            var vectormover = diferencia - diferencia.normalized;

            //moverse en casillas 
            vectormover = new Vector2(Mathf.Round(vectormover.x), Mathf.Round(vectormover.y));

            //moverse
            yield return character.Move(vectormover);

            StartCoroutine(DialogManager.Instance.ShowDialog(dialog, () =>
            {
                print("Batalla");
            }));
        }
}
