using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{

    private int menu;
    private int min;
    private int max;

    [SerializeField]
    private GameObject cursor;
    [SerializeField]
    private GameObject text;

    //___________________________//

    [SerializeField] GameObject itemList;
    [SerializeField] ItemSlotUI itemSlotUI;

    [SerializeField] Image image;
    [SerializeField] Text descText;

    List<ItemSlotUI> slotUIList;
    Inventario inventario;

    private void Awake()
    {
        inventario = Inventario.GetInventario();
    }
    private void OnEnable()
    {

        text.transform.position = new Vector3(720f, 1020f, 0);
        menu = 0;
        max = menu + 4;
        min = menu - 4;

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
                actualizarCursor();
            }

            else if (menu < min)
            {


                text.transform.position = new Vector3(text.transform.position.x, text.transform.position.y - 120, text.transform.position.z);
                min--;
                max = menu + 4;


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
                actualizarCursor();
            }
            if (menu > max)
            {
                text.transform.position = new Vector3(text.transform.position.x, text.transform.position.y + 120, text.transform.position.z);
                max++;
                min = menu - 4;
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

        print(inventario.Slots[menu].Item);
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

        slotUIList = new List<ItemSlotUI>();
        foreach (var itemSlot in inventario.Slots)
        {
            var slotUIObj = Instantiate(itemSlotUI, itemList.transform);
            slotUIObj.setData(itemSlot);

            slotUIList.Add(slotUIObj);
        }


        // GetComponent<DescriptionUI>().setInfo(inventario.Slots[menu].Item);

    }
    void UpdateItemSelection()
    {
        

        var item = inventario.Slots[menu].Item;
        image.sprite = item.Icon;
        descText.text = item.Description;
       


    }
}
