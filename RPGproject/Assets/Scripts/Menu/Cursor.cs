using UnityEngine;

public class Cursor : MonoBehaviour
{
    Vector2 Pos;
    public int menu;
    [SerializeField]
    private GameObject cursor;

    [SerializeField]
    private GameObject text;


    void awake()
    {
        menu = 0;
        Pos = transform.position;

        

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        actualizarCursor();
    }

    public void Move()
    {
        print(menu);
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            menu--;


            if (menu == -1)
                menu = 6;

            menu = menu % text.transform.childCount;

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            menu++;
            menu = menu % text.transform.childCount;

        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (menu == 1)
            {
                GetComponent<PokemonEvent>().OnMenu();
            }
            if (menu == 2)
            {
                GetComponent<MenuEvent>().OnMenu();
            }
            if (menu == 4)
            {
                SavingSystem.i.Save("saveSlot1");
                GetComponent<PlayerEvent>().OnMenu();
            }
            if (menu == 3)
            {
                SavingSystem.i.Load("saveSlot1");
                GetComponent<PlayerEvent>().OnMenu();
            }
           

        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {

            GetComponent<PlayerEvent>().OnMenu();

        }



    }
    public void actualizarCursor()
    {

        cursor.transform.position = text.transform.GetChild(menu).position;

    }
}
