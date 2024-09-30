using UnityEngine;
using UnityEngine.SceneManagement;

public enum Map { Map_00_MainMenu, Map_01_RealRoom, Map_02_DreamRoom };

public class GameManager_Script : MonoBehaviour
{
    public static GameManager_Script I { get; private set; }

    Map currentMap;
    private void Awake()
    {
        if (I != null && I != this)
        {
            Destroy(this);
        }
        else
        {
            I = this;
        }
        DontDestroyOnLoad(this);
        currentMap = Map.Map_00_MainMenu;
    }

    public void ChangeLevel(Map mapname)
    {
        SceneManager.LoadScene(mapname.ToString());
    }


    public void SaveGame()
    {

    }

    public void LoadGame() 
    {
    
    }

}
