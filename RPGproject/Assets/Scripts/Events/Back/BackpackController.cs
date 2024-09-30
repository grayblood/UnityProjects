using UnityEngine;

public class BackpackController : MonoBehaviour
{
 
    public int menu;
    

  

    void awake()
    {
        menu = 1;
       
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Move()
    {
       
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            menu--;


            if (menu == -1)
                menu = 2;


        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            menu++;
            if (menu == 3)
            {
                menu = 0;
            }

        }


        for(int i = 1; i < this.transform.childCount; i++)
        {
            if (i == menu + 1)
            {
                this.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                this.transform.GetChild(i).gameObject.SetActive(false);
            }
        }



        if (Input.GetKeyDown(KeyCode.Escape))
        {

            GetComponent<MenuEvent>().OnMenu();

        }



    }


    
}
