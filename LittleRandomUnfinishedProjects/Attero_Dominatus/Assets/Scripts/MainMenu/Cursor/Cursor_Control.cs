using System.Collections.Generic;
using UnityEngine;

public class Cursor_Control : MonoBehaviour
{
    List<Vector2> positions = new List<Vector2> { new Vector2(0, 350f), new Vector2(0, 250f), new Vector2(0, 150f) };
    RectTransform m_RectTransform;
    int m_Selected_Pos = 0;
    private void Start()
    {
        m_RectTransform = GetComponent<RectTransform>();
        changePos();
    }
    private void Update()
    {
        moveCursor();
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Z))
        {
            if(m_Selected_Pos == 0)
            {
                //Change to load save file Image
            }
            if (m_Selected_Pos == 1)
            {
                GameManager_Script.I.ChangeLevel(Map.Map_01_RealRoom);
            }
            if (m_Selected_Pos == 2)
            {
                Application.Quit();
            }
        }
    }
    void moveCursor()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_Selected_Pos--;
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            m_Selected_Pos++;
        }
        if (m_Selected_Pos > 2)
        {
            m_Selected_Pos = 0;

        }
        if (m_Selected_Pos < 0)
        {
            m_Selected_Pos = 2;

        }
        changePos();
    }

    void changePos()
    {
        m_RectTransform.anchoredPosition = positions[m_Selected_Pos];
    }
}
