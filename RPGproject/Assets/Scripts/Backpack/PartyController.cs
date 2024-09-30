using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyController : MonoBehaviour
{


    private int menu;


    [SerializeField]
    private GameObject cursor;
    [SerializeField]
    private GameObject text;

    //___________________________//

    [SerializeField] GameObject itemList;
    [SerializeField] PartySlotUI itemSlotUI;

    [SerializeField] Image image;
    [SerializeField] Text descText;

    List<PartySlotUI> slotUIList;
    PokemonParty pokemonParty;

    private void Awake()
    {
        pokemonParty = PokemonParty.GetPokemonParty();
    }
    private void OnEnable()
    {

        menu = 0;
       
        actualizarCursor();
        UpdateItemSelection();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        //actualizarCursor();
    }

    public void Move()
    {
        int prevSelection = menu;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {

            menu--;


            if (menu == -1)
            {

                menu++;

            }
            else
            {
                actualizarCursor();
            }



        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {

            menu++;
            if (menu == text.transform.childCount)
            {
                menu--;

            }

            else
            {
                actualizarCursor();
            }


        }



        if (Input.GetKeyDown(KeyCode.Z))
        {


        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {

            GetComponent<MenuEvent>().OnMenu();

        }
        if (prevSelection != menu)
            UpdateItemSelection();


    }
    public void actualizarCursor()
    {

        cursor.transform.position = new Vector3(cursor.transform.position.x, text.transform.GetChild(menu).position.y, 0);

        print(pokemonParty.PokeSlots[menu]);
    }





    private void Start()
    {
        UpdateItemList();
    }

    void UpdateItemList()
    {
        //clear

        foreach (Transform child in itemList.transform)
            Destroy(child.gameObject);

        slotUIList = new List<PartySlotUI>();
        foreach (var itemSlot in pokemonParty.PokeSlots)
        {
            var slotUIObj = Instantiate(itemSlotUI, itemList.transform);
            slotUIObj.setData(itemSlot);

            slotUIList.Add(slotUIObj);
        }


        // GetComponent<DescriptionUI>().setInfo(inventario.Slots[menu].Item);

    }
    void UpdateItemSelection()
    {


        var poke = pokemonParty.PokeSlots[menu];
        image.sprite = poke.sprite;
        descText.text = poke.name;



    }
}
