using UnityEngine;
using Mirror;

[AddComponentMenu("")]
public class NetworkRoomManagerExt : NetworkRoomManager
{
    public override void OnRoomServerSceneChanged(string sceneName)
    {
        if (sceneName == GameplayScene)
            Spawner.InitialSpawn();
    }

    public override void OnRoomStopClient()
    {
        base.OnRoomStopClient();
    }

    public override void OnRoomStopServer()
    {
        base.OnRoomStopServer();
    }

    bool showStartButton;

    public override void OnRoomServerPlayersReady()
    {
#if UNITY_SERVER
        base.OnRoomServerPlayersReady();
#else
        showStartButton = true;
#endif
    }

    public override void OnGUI()
    {
        base.OnGUI();

        if (allPlayersReady && showStartButton && GUI.Button(new Rect(150, 300, 120, 20), "START GAME"))
        {
            showStartButton = false;

            ServerChangeScene(GameplayScene);
        }
    }
}

